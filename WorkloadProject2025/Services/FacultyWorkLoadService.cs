using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class FacultyWorkLoadService
    {
        private readonly ApplicationDbContext _context;

        public FacultyWorkLoadService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Copy all workloads for a faculty (or all faculty when facultyEmail is null) from one term to another
        public async Task<int> CopyWorkloadsAsync(int fromTermId, int toTermId, string? facultyEmail = null, CancellationToken ct = default)
        {
            if (fromTermId == toTermId) throw new ArgumentException("Source and target terms must differ");

            var query = _context.FacultyWorkLoads.AsQueryable().Where(w => w.TermId == fromTermId);
            if (!string.IsNullOrWhiteSpace(facultyEmail))
            {
                query = query.Where(w => w.FacultyEmail == facultyEmail);
            }
            var source = await query.ToListAsync(ct);
            if (source.Count == 0) return 0;

            foreach (var w in source)
            {
                var copy = new FacultyWorkLoad
                {
                    FacultyEmail = w.FacultyEmail,
                    CourseId = w.CourseId,
                    ProgramOfStudyId = w.ProgramOfStudyId,
                    TermId = toTermId,
                    WorkloadCategoryId = w.WorkloadCategoryId,
                    Workload = w.Workload,
                    HoursAssigned = w.HoursAssigned,
                    Description = w.Description,
                    DateAssigned = DateTime.Now,
                    Notes = w.Notes,
                    IsPrimaryInstructor = w.IsPrimaryInstructor,
                    PercentShare = w.PercentShare,
                    IsCoverage = w.IsCoverage,
                    CoveredForFacultyEmail = w.CoveredForFacultyEmail,
                    HRProcessed = false,
                    HRProcessedDate = null,
                    HRNotes = null
                };
                _context.FacultyWorkLoads.Add(copy);
            }
            return await _context.SaveChangesAsync(ct);
        }

        public Task<List<FacultyWorkLoad>> GetByTermAsync(int termId, CancellationToken ct = default)
        {
            return _context.FacultyWorkLoads
                .Where(w => w.TermId == termId)
                .Include(w => w.Faculty)
                .Include(w => w.Course)
                .Include(w => w.ProgramOfStudy)
                .Include(w => w.WorkloadCategory)
                .ToListAsync(ct);
        }

        // Bulk update helper to mass-set fields on selected workload items
        public async Task<int> BulkUpdateAsync(BulkUpdateRequest request, CancellationToken ct = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Ids == null || request.Ids.Count == 0) return 0;
            var items = await _context.FacultyWorkLoads.Where(w => request.Ids.Contains(w.Id)).ToListAsync(ct);
            foreach (var w in items)
            {
                if (!string.IsNullOrWhiteSpace(request.FacultyEmail)) w.FacultyEmail = request.FacultyEmail!;
                if (request.TermId.HasValue) w.TermId = request.TermId.Value;
                if (request.CourseId.HasValue) w.CourseId = request.CourseId;
                if (request.ProgramOfStudyId.HasValue) w.ProgramOfStudyId = request.ProgramOfStudyId;
                if (request.WorkloadCategoryId.HasValue) w.WorkloadCategoryId = request.WorkloadCategoryId;
                if (request.Workload.HasValue) w.Workload = request.Workload.Value;
                if (request.HoursAssigned.HasValue) w.HoursAssigned = request.HoursAssigned.Value;
                if (request.IsPrimaryInstructor.HasValue) w.IsPrimaryInstructor = request.IsPrimaryInstructor.Value;
                if (request.PercentShareHasValue)
                {
                    w.PercentShare = request.PercentShare; // can be null explicitly
                }
                if (!string.IsNullOrWhiteSpace(request.Notes)) w.Notes = request.Notes;
                // HR fields
                if (request.HRProcessedHasValue)
                {
                    w.HRProcessed = request.HRProcessed ?? w.HRProcessed;
                    if (request.HRProcessed == true && !request.HRProcessedDateHasValue)
                    {
                        w.HRProcessedDate = DateTime.Now;
                    }
                }
                if (request.HRProcessedDateHasValue)
                {
                    w.HRProcessedDate = request.HRProcessedDate;
                }
                if (!string.IsNullOrWhiteSpace(request.HRNotes))
                {
                    w.HRNotes = request.HRNotes;
                }
            }
            return await _context.SaveChangesAsync(ct);
        }

        // Simple CSV import. Expected headers (any order):
        // FacultyEmail,CourseId,CourseName,Workload,HoursAssigned,ProgramOfStudyId,ProgramName,IsPrimaryInstructor,PercentShare,Description,Notes
        // Term is provided separately as termId
        public async Task<ImportResult> ImportFromCsvAsync(int termId, string csv, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(csv)) return new ImportResult();
            var lines = csv.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) return new ImportResult();
            var header = lines[0].Split(',', StringSplitOptions.TrimEntries);
            var map = header.Select((h, i) => (h: h.ToLowerInvariant(), i)).ToDictionary(x => x.h, x => x.i);
            var result = new ImportResult();
            for (int li = 1; li < lines.Length; li++)
            {
                try
                {
                    var cols = SplitCsvLine(lines[li]);
                    string facultyEmail = Get<string>(cols, map, "facultyemail")!;
                    if (string.IsNullOrWhiteSpace(facultyEmail)) { result.Skipped++; continue; }
                    int? courseId = Get<int?>(cols, map, "courseid");
                    string? courseName = Get<string?>(cols, map, "coursename");
                    int? programId = Get<int?>(cols, map, "programofstudyid");
                    string? programName = Get<string?>(cols, map, "programname");
                    string workloadStr = Get<string>(cols, map, "workload") ?? string.Empty;
                    decimal hours = Get<decimal>(cols, map, "hoursassigned");
                    bool? isPrimary = Get<bool?>(cols, map, "isprimaryinstructor");
                    decimal? percentShare = Get<decimal?>(cols, map, "percentshare");
                    string? description = Get<string?>(cols, map, "description");
                    string? notes = Get<string?>(cols, map, "notes");

                    // Resolve workload enum
                    if (!Enum.TryParse<Workload>(workloadStr.Replace(" ", "_"), true, out var workload))
                    {
                        throw new Exception($"Invalid Workload '{workloadStr}'");
                    }

                    // Resolve course if only name provided
                    if (!courseId.HasValue && !string.IsNullOrWhiteSpace(courseName))
                    {
                        var c = await _context.Courses.FirstOrDefaultAsync(x => x.Name == courseName, ct);
                        courseId = c?.Id;
                    }
                    // Resolve program if only name provided
                    if (!programId.HasValue && !string.IsNullOrWhiteSpace(programName))
                    {
                        var p = await _context.ProgramsOfStudy.FirstOrDefaultAsync(x => x.Name == programName, ct);
                        programId = p?.Id;
                    }

                    var entity = new FacultyWorkLoad
                    {
                        FacultyEmail = facultyEmail,
                        CourseId = courseId,
                        ProgramOfStudyId = programId,
                        TermId = termId,
                        Workload = workload,
                        HoursAssigned = hours,
                        Description = description,
                        Notes = notes,
                        IsPrimaryInstructor = isPrimary ?? true,
                        PercentShare = percentShare,
                        DateAssigned = DateTime.Now
                    };
                    _context.FacultyWorkLoads.Add(entity);
                    result.Imported++;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Line {li + 1}: {ex.Message}");
                }
            }
            await _context.SaveChangesAsync(ct);
            return result;
        }

        private static T? Get<T>(string[] cols, Dictionary<string, int> map, string key)
        {
            if (!map.TryGetValue(key, out var idx)) return default;
            var raw = idx < cols.Length ? cols[idx] : null;
            if (string.IsNullOrWhiteSpace(raw)) return default;
            try
            {
                return (T?)Convert.ChangeType(raw, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
            }
            catch
            {
                return default;
            }
        }

        private static string[] SplitCsvLine(string line)
        {
            // naive split that handles simple quoted fields
            var result = new List<string>();
            bool inQuotes = false;
            var current = new System.Text.StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                var ch = line[i];
                if (ch == '"') { inQuotes = !inQuotes; continue; }
                if (ch == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(ch);
                }
            }
            result.Add(current.ToString());
            return result.ToArray();
        }
    }

    public class BulkUpdateRequest
    {
        public List<int> Ids { get; set; } = new();
        public string? FacultyEmail { get; set; }
        public int? TermId { get; set; }
        public int? CourseId { get; set; }
        public int? ProgramOfStudyId { get; set; }
        public int? WorkloadCategoryId { get; set; }
        public Workload? Workload { get; set; }
        public decimal? HoursAssigned { get; set; }
        public bool? IsPrimaryInstructor { get; set; }
        public decimal? PercentShare { get; set; }
        public bool PercentShareHasValue { get; set; } // distinguishes between not set and explicit null
        public string? Notes { get; set; }
        // HR fields
        public bool? HRProcessed { get; set; }
        public bool HRProcessedHasValue { get; set; }
        public DateTime? HRProcessedDate { get; set; }
        public bool HRProcessedDateHasValue { get; set; }
        public string? HRNotes { get; set; }
    }

    public class ImportResult
    {
        public int Imported { get; set; }
        public int Skipped { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
