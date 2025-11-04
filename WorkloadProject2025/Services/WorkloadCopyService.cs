using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
 public class WorkloadCopyService : IWorkloadCopyService
 {
 private readonly ApplicationDbContext _db;
 public WorkloadCopyService(ApplicationDbContext db)
 {
 _db = db;
 }

 public async Task<WorkloadCopyResult> CopyWorkloadAsync(int sourceTermId, int targetTermId, CancellationToken cancellationToken = default)
 {
 var result = new WorkloadCopyResult { SourceTermId = sourceTermId, TargetTermId = targetTermId };
 if (sourceTermId == targetTermId)
 {
 result.Errors.Add("Source and target term must be different.");
 return result;
 }

 var source = await _db.FacultyWorkLoads
 .AsNoTracking()
 .Where(w => w.TermId == sourceTermId)
 .ToListAsync(cancellationToken);

 result.TotalFound = source.Count;
 if (source.Count ==0) return result;

 foreach (var item in source)
 {
 // duplicate guard: same FacultyEmail + CourseId + TargetTermId
 var duplicate = await _db.FacultyWorkLoads.AnyAsync(w =>
 w.FacultyEmail == item.FacultyEmail &&
 w.CourseId == item.CourseId &&
 w.TermId == targetTermId,
 cancellationToken);
 if (duplicate)
 {
 result.SkippedDuplicates++;
 continue;
 }

 var copy = new FacultyWorkLoad
 {
 FacultyEmail = item.FacultyEmail,
 CourseId = item.CourseId,
 TermId = targetTermId,
 WorkloadCategoryId = item.WorkloadCategoryId,
 Workload = item.Workload,
 HoursAssigned = item.HoursAssigned,
 Description = item.Description,
 DateAssigned = DateTime.Now
 };
 _db.FacultyWorkLoads.Add(copy);
 result.Copied++;
 }

 await _db.SaveChangesAsync(cancellationToken);
 return result;
 }
 }
}
