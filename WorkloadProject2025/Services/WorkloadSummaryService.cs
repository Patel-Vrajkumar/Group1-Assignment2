using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
 public class WorkloadSummaryService
 {
 private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
 public WorkloadSummaryService(IDbContextFactory<ApplicationDbContext> contextFactory) => _contextFactory = contextFactory;

 public async Task<InstructorSummary> GetInstructorSummaryAsync(string facultyEmail, int? termId = null, CancellationToken ct = default)
 {
 await using var context = await _contextFactory.CreateDbContextAsync(ct);
 var q = context.FacultyWorkLoads.AsQueryable().Where(w => w.FacultyEmail == facultyEmail);
 if (termId.HasValue) q = q.Where(w => w.TermId == termId.Value);
 var items = await q.ToListAsync(ct);
 return new InstructorSummary
 {
 FacultyEmail = facultyEmail,
 TotalHours = items.Sum(EffectiveHours),
 ProgramSubtotals = items
 .GroupBy(i => i.ProgramOfStudyId)
 .ToDictionary(g => g.Key, g => g.Sum(EffectiveHours))
 };
 }

 public async Task<GlobalSummary> GetGlobalSummaryAsync(int? termId = null, CancellationToken ct = default)
 {
 await using var context = await _contextFactory.CreateDbContextAsync(ct);
 var q = context.FacultyWorkLoads.AsQueryable();
 if (termId.HasValue) q = q.Where(w => w.TermId == termId.Value);
 var items = await q.ToListAsync(ct);
 return new GlobalSummary
 {
 GrandTotalHours = items.Sum(EffectiveHours),
 ProgramTotals = items
 .GroupBy(i => i.ProgramOfStudyId)
 .ToDictionary(g => g.Key, g => g.Sum(EffectiveHours))
 };
 }

 private static decimal EffectiveHours(FacultyWorkLoad i)
 {
 var share = i.PercentShare.HasValue ? (i.PercentShare.Value/100m) :1m;
 return i.HoursAssigned * share;
 }
 }

 public class InstructorSummary
 {
 public string FacultyEmail { get; set; } = string.Empty;
 public decimal TotalHours { get; set; }
 public Dictionary<int?, decimal> ProgramSubtotals { get; set; } = new();
 }

 public class GlobalSummary
 {
 public decimal GrandTotalHours { get; set; }
 public Dictionary<int?, decimal> ProgramTotals { get; set; } = new();
 }
}
