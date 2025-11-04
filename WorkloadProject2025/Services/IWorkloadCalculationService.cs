using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
 public record FacultyWorkloadSummary(decimal TotalHours, int AssignmentCount, int Violations);

 public interface IWorkloadCalculationService
 {
 FacultyWorkloadSummary GetSummaryForFaculty(string facultyEmail, IEnumerable<FacultyWorkLoad> allWorkloads);
 bool IsAssignmentOutOfRange(FacultyWorkLoad workload);
 }
}
