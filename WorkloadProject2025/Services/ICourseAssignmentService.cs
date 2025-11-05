using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services;

public interface ICourseAssignmentService
{
 Task<CourseAssignment> AssignInstructorAsync(CourseAssignment assignment);
 Task<bool> CanAssignAsync(string facultyEmail, int termId, decimal assignmentCredit, int? courseId = null);
 Task<decimal> GetWorkloadCreditsAsync(string facultyEmail, int termId);
}
