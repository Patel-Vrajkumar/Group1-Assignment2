using WorkloadProject2025.Data.Models;
using WorkloadProject2025.Data.Models.DTOs;

namespace WorkloadProject2025.Services
{
    public interface IFacultyWorkLoadService
    {
    // Basic CRUD
    Task<List<FacultyWorkLoad>> GetAllAsync();
    Task<FacultyWorkLoad?> GetByIdAsync(int id);
  Task<FacultyWorkLoad> CreateAsync(FacultyWorkLoad workload, string currentUser);
        Task<FacultyWorkLoad> UpdateAsync(FacultyWorkLoad workload, string currentUser);
        Task<bool> DeleteAsync(int id);
        
        // Advanced queries
    Task<List<FacultyWorkLoad>> GetByFacultyEmailAsync(string email);
        Task<List<FacultyWorkLoad>> GetByTermAsync(int termId);
    Task<List<FacultyWorkLoad>> GetByStatusAsync(WorkloadStatus status);
        Task<decimal> GetTotalHoursForFacultyAsync(string email, int termId);
  
        // Cloning
     Task<List<FacultyWorkLoad>> CloneWorkloadsAsync(WorkloadCloneRequest request, string currentUser);
 
        // Bulk operations
        Task<int> BulkUpdateAsync(BulkUpdateRequest request, string currentUser);
     Task<int> BulkDeleteAsync(List<int> workloadIds);
        
  // Import/Export
        Task<List<WorkloadImportDto>> ValidateImportDataAsync(List<WorkloadImportDto> data);
        Task<int> ImportWorkloadsAsync(List<WorkloadImportDto> validatedData, string currentUser);
        Task<byte[]> ExportToExcelAsync(List<FacultyWorkLoad> workloads);
   Task<byte[]> GenerateImportTemplateAsync();
        
   // Validation & Conflict Detection
        Task<List<string>> ValidateWorkloadAsync(FacultyWorkLoad workload);
     Task<bool> CheckDoubleBookingAsync(string facultyEmail, int termId, int? excludeWorkloadId = null);
 Task<bool> IsFacultyOverloadedAsync(string facultyEmail, int termId);
    
        // Auto-save
   Task<FacultyWorkLoad> SaveDraftAsync(FacultyWorkLoad workload, string currentUser);
        Task<List<FacultyWorkLoad>> GetDraftsAsync(string currentUser);
    }
}
