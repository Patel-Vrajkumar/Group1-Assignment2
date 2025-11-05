using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services;

public class WorkloadCalculationService(ApplicationDbContext context) : IWorkloadCalculationService
{
 private readonly ApplicationDbContext _context = context;

 public async Task<decimal> GetTotalHoursForFacultyAsync(string facultyEmail, int? termId = null)
 {
 var query = _context.FacultyWorkLoads.AsQueryable().Where(w => w.FacultyEmail == facultyEmail);
 if (termId.HasValue) query = query.Where(w => w.TermId == termId.Value);
 return await query.SumAsync(w => (decimal?)w.HoursAssigned) ??0m;
 }

 public async Task<(decimal teaching, decimal nonTeaching)> GetBreakdownAsync(string facultyEmail, int termId)
 {
 var data = await _context.FacultyWorkLoads
 .Where(w => w.FacultyEmail == facultyEmail && w.TermId == termId)
 .Select(w => new { w.HoursAssigned, w.Workload })
 .ToListAsync();

 decimal teaching =0m, nonTeaching =0m;
 foreach (var x in data)
 {
 switch (x.Workload)
 {
 case Workload.Course_Lecture:
 case Workload.Course_Lab:
 teaching += x.HoursAssigned; break;
 default:
 nonTeaching += x.HoursAssigned; break;
 }
 }
 return (teaching, nonTeaching);
 }

 public async Task<Dictionary<int, decimal>> GetProgramSubtotalsAsync(string facultyEmail, int termId)
 {
 // Hours by program of study using course link when present
 var items = await _context.FacultyWorkLoads
 .Include(w => w.Course)!
 .Where(w => w.FacultyEmail == facultyEmail && w.TermId == termId)
 .ToListAsync();

 var dict = new Dictionary<int, decimal>();
 foreach (var w in items)
 {
 var posId = w.Course?.ProgramOfStudyId;
 if (!posId.HasValue) continue; // only count program-linked workloads here
 dict[posId.Value] = dict.GetValueOrDefault(posId.Value) + w.HoursAssigned;
 }
 return dict;
 }

 public async Task<(CapacityStatus status, decimal percent)> GetCapacityAsync(string facultyEmail, int termId, int? programId = null)
 {
 var faculty = await _context.Faculty.FirstOrDefaultAsync(f => f.Email == facultyEmail) ?? throw new InvalidOperationException("Faculty not found");
 var (teaching, nonTeaching) = await GetBreakdownAsync(facultyEmail, termId);
 var total = teaching + nonTeaching;
 var max = faculty.MaxTeachingHours; // using hours-based max already in model
 var percent = max <=0 ?0 : Math.Round((total / max) *100m,2);
 var status = percent switch
 {
 >=100m => CapacityStatus.Overloaded,
 >=80m => CapacityStatus.NearCapacity,
 >=40m => CapacityStatus.Optimal,
 _ => CapacityStatus.Underutilized
 };
 return (status, percent);
 }

 public async Task<FacultyWorkloadSummary> GetSummaryAsync(string facultyEmail, int termId)
 {
 var (teaching, nonTeaching) = await GetBreakdownAsync(facultyEmail, termId);
 var byProgram = await GetProgramSubtotalsAsync(facultyEmail, termId);
 return new FacultyWorkloadSummary(
 facultyEmail,
 teaching,
 nonTeaching,
 teaching + nonTeaching,
 byProgram);
 }
}
