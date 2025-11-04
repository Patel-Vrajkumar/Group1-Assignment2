using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
 public class WorkloadCalculationService : IWorkloadCalculationService
 {
 public FacultyWorkloadSummary GetSummaryForFaculty(string facultyEmail, IEnumerable<FacultyWorkLoad> allWorkloads)
 {
 var list = allWorkloads.Where(w => w.FacultyEmail == facultyEmail).ToList();
 var total = list.Sum(w => w.HoursAssigned);
 var count = list.Count;
 var violations = list.Count(IsAssignmentOutOfRange);
 return new FacultyWorkloadSummary(total, count, violations);
 }

 public bool IsAssignmentOutOfRange(FacultyWorkLoad workload)
 {
 var cat = workload.WorkloadCategory;
 if (cat == null || !workload.WorkloadCategoryId.HasValue) return false;
 return workload.HoursAssigned < cat.MiniumHours || workload.HoursAssigned > cat.MaximumHours;
 }
 }
}
