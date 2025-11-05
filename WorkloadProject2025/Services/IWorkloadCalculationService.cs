using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services;

public record FacultyWorkloadSummary(
 string FacultyEmail,
 decimal TeachingHours,
 decimal NonTeachingHours,
 decimal TotalHours,
 Dictionary<int, decimal> HoursByProgram // ProgramOfStudyId -> hours
);

public enum CapacityStatus
{
 Underutilized,
 Optimal,
 NearCapacity,
 Overloaded
}

public interface IWorkloadCalculationService
{
 Task<decimal> GetTotalHoursForFacultyAsync(string facultyEmail, int? termId = null);
 Task<(decimal teaching, decimal nonTeaching)> GetBreakdownAsync(string facultyEmail, int termId);
 Task<Dictionary<int, decimal>> GetProgramSubtotalsAsync(string facultyEmail, int termId);
 Task<(CapacityStatus status, decimal percent)> GetCapacityAsync(string facultyEmail, int termId, int? programId = null);
 Task<FacultyWorkloadSummary> GetSummaryAsync(string facultyEmail, int termId);
}
