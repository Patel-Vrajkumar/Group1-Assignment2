using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
 public class WorkloadCopyResult
 {
 public int SourceTermId { get; set; }
 public int TargetTermId { get; set; }
 public int TotalFound { get; set; }
 public int Copied { get; set; }
 public int SkippedDuplicates { get; set; }
 public List<string> Errors { get; set; } = new();
 }

 public interface IWorkloadCopyService
 {
 Task<WorkloadCopyResult> CopyWorkloadAsync(int sourceTermId, int targetTermId, CancellationToken cancellationToken = default);
 }
}
