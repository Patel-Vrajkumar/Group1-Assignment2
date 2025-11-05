using Microsoft.EntityFrameworkCore;
using System.Linq;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services;

public class CourseAssignmentService(ApplicationDbContext context) : ICourseAssignmentService
{
 private readonly ApplicationDbContext _context = context;

 public async Task<CourseAssignment> AssignInstructorAsync(CourseAssignment assignment)
 {
 //1 course ==1 credit; percentage maps to fractional credit
 var credit = Math.Clamp(assignment.AssignmentPercentage /100m,0m,1m);
 var canAssign = await CanAssignAsync(assignment.InstructorEmail, assignment.TermId, credit, assignment.CourseId);
 if (!canAssign)
 throw new InvalidOperationException("Assignment would exceed instructor max course load for the term.");

 _context.CourseAssignments.Add(assignment);
 await _context.SaveChangesAsync();
 return assignment;
 }

 public async Task<bool> CanAssignAsync(string facultyEmail, int termId, decimal assignmentCredit, int? courseId = null)
 {
 var faculty = await _context.Faculty.FirstOrDefaultAsync(f => f.Email == facultyEmail);
 if (faculty is null) return false;

 var currentCredits = await GetWorkloadCreditsAsync(facultyEmail, termId);
 return currentCredits + assignmentCredit <= faculty.MaxCourseLoad;
 }

 public async Task<decimal> GetWorkloadCreditsAsync(string facultyEmail, int termId)
 {
 var items = await _context.CourseAssignments
 .Where(a => a.InstructorEmail == facultyEmail && a.TermId == termId && a.IsActive)
 .ToListAsync();

 if (items.Count ==0) return 0m;
 return items.Sum(i => Math.Clamp(i.AssignmentPercentage /100m,0m,1m));
 }
}
