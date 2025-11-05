using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Data.Models.DTOs;
using ClosedXML.Excel;

namespace WorkloadProject2025.Services
{
    public class FacultyWorkLoadService : IFacultyWorkLoadService
    {
        private readonly ApplicationDbContext _context;

        public FacultyWorkLoadService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Basic CRUD
        public async Task<List<FacultyWorkLoad>> GetAllAsync()
        {
            return await _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.Term)
                .Include(w => w.WorkloadCategory)
                .OrderByDescending(w => w.DateAssigned)
                .ToListAsync();
        }

        public async Task<FacultyWorkLoad?> GetByIdAsync(int id)
        {
            return await _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.Term)
                .Include(w => w.WorkloadCategory)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<FacultyWorkLoad> CreateAsync(FacultyWorkLoad workload, string currentUser)
        {
            workload.CreatedBy = currentUser;
            workload.CreatedDate = DateTime.Now;
            workload.LastModifiedBy = currentUser;
            workload.LastModifiedDate = DateTime.Now;

            _context.FacultyWorkLoads.Add(workload);
            await _context.SaveChangesAsync();
            return workload;
        }

        public async Task<FacultyWorkLoad> UpdateAsync(FacultyWorkLoad workload, string currentUser)
        {
            workload.LastModifiedBy = currentUser;
            workload.LastModifiedDate = DateTime.Now;

            _context.FacultyWorkLoads.Update(workload);
            await _context.SaveChangesAsync();
            return workload;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var workload = await _context.FacultyWorkLoads.FindAsync(id);
            if (workload == null) return false;

            _context.FacultyWorkLoads.Remove(workload);
            await _context.SaveChangesAsync();
            return true;
        }

        // Advanced queries
        public async Task<List<FacultyWorkLoad>> GetByFacultyEmailAsync(string email)
        {
            return await _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.Term)
                .Include(w => w.WorkloadCategory)
                .Where(w => w.FacultyEmail == email)
                .ToListAsync();
        }

        public async Task<List<FacultyWorkLoad>> GetByTermAsync(int termId)
        {
            return await _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.Term)
                .Include(w => w.WorkloadCategory)
                .Where(w => w.TermId == termId)
                .ToListAsync();
        }

        public async Task<List<FacultyWorkLoad>> GetByStatusAsync(WorkloadStatus status)
        {
            return await _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.Term)
                .Include(w => w.WorkloadCategory)
                .Where(w => w.Status == status)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalHoursForFacultyAsync(string email, int termId)
        {
            return await _context.FacultyWorkLoads
                .Where(w => w.FacultyEmail == email && w.TermId == termId)
                .SumAsync(w => w.HoursAssigned);
        }

        // Cloning
        public async Task<List<FacultyWorkLoad>> CloneWorkloadsAsync(WorkloadCloneRequest request, string currentUser)
        {
            var query = _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Where(w => w.TermId == request.SourceTermId);

            // Apply filters
            if (request.FacultyEmails != null && request.FacultyEmails.Any())
            {
                query = query.Where(w => request.FacultyEmails.Contains(w.FacultyEmail));
            }

            if (request.DepartmentIds != null && request.DepartmentIds.Any())
            {
                // You'll need to add Department navigation to implement this
                // For now, skip this filter
            }

            var sourceWorkloads = await query.ToListAsync();
            var clonedWorkloads = new List<FacultyWorkLoad>();

            foreach (var source in sourceWorkloads)
            {
                var cloned = new FacultyWorkLoad
                {
                    FacultyEmail = source.FacultyEmail,
                    CourseId = source.CourseId,
                    TermId = request.TargetTermId,
                    WorkloadCategoryId = source.WorkloadCategoryId,
                    Workload = source.Workload,
                    HoursAssigned = source.HoursAssigned,
                    Description = source.Description,
                    Annotations = request.IncludeAnnotations ? source.Annotations : null,
                    Status = WorkloadStatus.Draft,
                    DateAssigned = DateTime.Now,
                    CreatedBy = currentUser,
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = currentUser,
                    LastModifiedDate = DateTime.Now,
                    ClonedFromId = source.Id,
                    SourceYear = await GetYearFromTermAsync(source.TermId)
                };

                clonedWorkloads.Add(cloned);
            }

            _context.FacultyWorkLoads.AddRange(clonedWorkloads);
            await _context.SaveChangesAsync();

            return clonedWorkloads;
        }

        private async Task<int> GetYearFromTermAsync(int termId)
        {
            var term = await _context.Terms.FindAsync(termId);
            return term?.Year ?? DateTime.Now.Year;
        }

        // Bulk operations
        public async Task<int> BulkUpdateAsync(BulkUpdateRequest request, string currentUser)
        {
            var workloads = await _context.FacultyWorkLoads
                .Where(w => request.WorkloadIds.Contains(w.Id))
                .ToListAsync();

            foreach (var workload in workloads)
            {
                if (request.HoursAssigned.HasValue)
                    workload.HoursAssigned = request.HoursAssigned.Value;

                if (request.Status.HasValue)
                    workload.Status = request.Status.Value;

                if (!string.IsNullOrWhiteSpace(request.Annotations))
                {
                    if (request.AppendAnnotations && !string.IsNullOrWhiteSpace(workload.Annotations))
                    {
                        workload.Annotations += "\n" + request.Annotations;
                    }
                    else
                    {
                        workload.Annotations = request.Annotations;
                    }
                }

                workload.LastModifiedBy = currentUser;
                workload.LastModifiedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return workloads.Count;
        }

        public async Task<int> BulkDeleteAsync(List<int> workloadIds)
        {
            var workloads = await _context.FacultyWorkLoads
                .Where(w => workloadIds.Contains(w.Id))
                .ToListAsync();

            _context.FacultyWorkLoads.RemoveRange(workloads);
            await _context.SaveChangesAsync();

            return workloads.Count;
        }

        // Import/Export
        public async Task<List<WorkloadImportDto>> ValidateImportDataAsync(List<WorkloadImportDto> data)
        {
            var facultyEmails = await _context.Faculty.Select(f => f.Email).ToListAsync();
            var courseNames = await _context.Courses.Select(c => c.Name).ToListAsync();
            var termNames = await _context.Terms.Select(t => t.Name).ToListAsync();

            foreach (var item in data)
            {
                if (!facultyEmails.Contains(item.FacultyEmail))
                    item.ValidationErrors.Add($"Faculty email '{item.FacultyEmail}' not found");

                if (!string.IsNullOrWhiteSpace(item.CourseName) && !courseNames.Contains(item.CourseName))
                    item.ValidationErrors.Add($"Course '{item.CourseName}' not found");

                if (!termNames.Contains(item.TermName))
                    item.ValidationErrors.Add($"Term '{item.TermName}' not found");

                if (item.HoursAssigned <= 0)
                    item.ValidationErrors.Add("Hours assigned must be greater than 0");

                if (!Enum.TryParse<Workload>(item.WorkloadType.Replace(" ", "_"), out _))
                    item.ValidationErrors.Add($"Invalid workload type '{item.WorkloadType}'");
            }

            return data;
        }

        public async Task<int> ImportWorkloadsAsync(List<WorkloadImportDto> validatedData, string currentUser)
        {
            var validData = validatedData.Where(d => d.IsValid).ToList();
            var workloads = new List<FacultyWorkLoad>();

            foreach (var item in validData)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Name == item.CourseName);
                var term = await _context.Terms.FirstOrDefaultAsync(t => t.Name == item.TermName);

                if (term == null) continue;

                var workload = new FacultyWorkLoad
                {
                    FacultyEmail = item.FacultyEmail,
                    CourseId = course?.Id,
                    TermId = term.Id,
                    Workload = Enum.Parse<Workload>(item.WorkloadType.Replace(" ", "_")),
                    HoursAssigned = item.HoursAssigned,
                    Description = item.Description,
                    Annotations = item.Annotations,
                    Status = WorkloadStatus.Draft,
                    DateAssigned = DateTime.Now,
                    CreatedBy = currentUser,
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = currentUser,
                    LastModifiedDate = DateTime.Now
                };

                workloads.Add(workload);
            }

            _context.FacultyWorkLoads.AddRange(workloads);
            await _context.SaveChangesAsync();

            return workloads.Count;
        }

        public async Task<byte[]> ExportToExcelAsync(List<FacultyWorkLoad> workloads)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Workloads");

            // Headers
            worksheet.Cell(1, 1).Value = "Faculty Email";
            worksheet.Cell(1, 2).Value = "Faculty Name";
            worksheet.Cell(1, 3).Value = "Course";
            worksheet.Cell(1, 4).Value = "Term";
            worksheet.Cell(1, 5).Value = "Workload Type";
            worksheet.Cell(1, 6).Value = "Hours Assigned";
            worksheet.Cell(1, 7).Value = "Status";
            worksheet.Cell(1, 8).Value = "Description";
            worksheet.Cell(1, 9).Value = "Annotations";
            worksheet.Cell(1, 10).Value = "Date Assigned";

            // Style headers
            var headerRow = worksheet.Range(1, 1, 1, 10);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue;

            // Data
            int row = 2;
            foreach (var workload in workloads)
            {
                worksheet.Cell(row, 1).Value = workload.FacultyEmail;
                worksheet.Cell(row, 2).Value = $"{workload.Faculty?.FirstName} {workload.Faculty?.LastName}";
                worksheet.Cell(row, 3).Value = workload.Course?.Name ?? "N/A";
                worksheet.Cell(row, 4).Value = workload.Term?.Name ?? "N/A";
                worksheet.Cell(row, 5).Value = workload.Workload.ToString().Replace("_", " ");
                worksheet.Cell(row, 6).Value = workload.HoursAssigned;
                worksheet.Cell(row, 7).Value = workload.Status.ToString();
                worksheet.Cell(row, 8).Value = workload.Description ?? "";
                worksheet.Cell(row, 9).Value = workload.Annotations ?? "";
                worksheet.Cell(row, 10).Value = workload.DateAssigned?.ToString("yyyy-MM-dd") ?? "";
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public Task<byte[]> GenerateImportTemplateAsync()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Workload Import Template");

            // Headers
            worksheet.Cell(1, 1).Value = "Faculty Email";
            worksheet.Cell(1, 2).Value = "Course Name";
            worksheet.Cell(1, 3).Value = "Term Name";
            worksheet.Cell(1, 4).Value = "Workload Type";
            worksheet.Cell(1, 5).Value = "Hours Assigned";
            worksheet.Cell(1, 6).Value = "Description";
            worksheet.Cell(1, 7).Value = "Annotations";

            // Style headers
            var headerRow = worksheet.Range(1, 1, 1, 7);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightGreen;

            // Sample data
            worksheet.Cell(2, 1).Value = "example@college.edu";
            worksheet.Cell(2, 2).Value = "Introduction to Programming";
            worksheet.Cell(2, 3).Value = "Fall 2025";
            worksheet.Cell(2, 4).Value = "Course_Lecture";
            worksheet.Cell(2, 5).Value = 3.0;
            worksheet.Cell(2, 6).Value = "Sample course assignment";
            worksheet.Cell(2, 7).Value = "First time teaching this course";

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return Task.FromResult(stream.ToArray());
        }

        // Validation & Conflict Detection
        public async Task<List<string>> ValidateWorkloadAsync(FacultyWorkLoad workload)
        {
            var errors = new List<string>();

            // Check if faculty exists
            var faculty = await _context.Faculty.FindAsync(workload.FacultyEmail);
            if (faculty == null)
            {
                errors.Add("Faculty member not found");
                return errors;
            }

            // Check for overload
            var totalHours = await GetTotalHoursForFacultyAsync(workload.FacultyEmail, workload.TermId);
            totalHours += workload.HoursAssigned;

            if (totalHours > faculty.MaxTeachingHours)
            {
                errors.Add($"Warning: Total hours ({totalHours}) exceeds maximum allowed ({faculty.MaxTeachingHours})");
                workload.Status = WorkloadStatus.Overloaded;
            }

            // Check for double booking (if there's a course)
            if (workload.CourseId.HasValue)
            {
                var isDoubleBooked = await CheckDoubleBookingAsync(workload.FacultyEmail, workload.TermId, workload.Id);
                if (isDoubleBooked)
                {
                    errors.Add("Warning: Faculty member may have conflicting assignments");
                }
            }

            return errors;
        }

        public async Task<bool> CheckDoubleBookingAsync(string facultyEmail, int termId, int? excludeWorkloadId = null)
        {
            var query = _context.FacultyWorkLoads
                .Where(w => w.FacultyEmail == facultyEmail && w.TermId == termId);

            if (excludeWorkloadId.HasValue)
            {
                query = query.Where(w => w.Id != excludeWorkloadId.Value);
            }

            var count = await query.CountAsync();
            return count > 0; // Simplified - in reality, you'd check schedules
        }

        public async Task<bool> IsFacultyOverloadedAsync(string facultyEmail, int termId)
        {
            var faculty = await _context.Faculty.FindAsync(facultyEmail);
            if (faculty == null) return false;

            var totalHours = await GetTotalHoursForFacultyAsync(facultyEmail, termId);
            return totalHours > faculty.MaxTeachingHours;
        }

        // Auto-save
        public async Task<FacultyWorkLoad> SaveDraftAsync(FacultyWorkLoad workload, string currentUser)
        {
            workload.Status = WorkloadStatus.Draft;

            if (workload.Id == 0)
            {
                return await CreateAsync(workload, currentUser);
            }
            else
            {
                return await UpdateAsync(workload, currentUser);
            }
        }

        public async Task<List<FacultyWorkLoad>> GetDraftsAsync(string currentUser)
        {
            return await _context.FacultyWorkLoads
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.Term)
                .Include(w => w.WorkloadCategory)
                .Where(w => w.Status == WorkloadStatus.Draft && w.CreatedBy == currentUser)
                .ToListAsync();
        }
    }
}
