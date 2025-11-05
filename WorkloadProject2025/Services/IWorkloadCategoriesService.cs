using WorkloadProject2025.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkloadProject2025.Services
{
    public interface IWorkloadCategoriesService
    {
        Task<List<WorkloadCategory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<WorkloadCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<WorkloadCategory> AddAsync(WorkloadCategory category, CancellationToken cancellationToken = default);
        Task<WorkloadCategory> UpdateAsync(WorkloadCategory category, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<WorkloadCategory?> GetCurrentForAsync(EmploymentCategory? employmentCategory, DateTime asOf, CancellationToken cancellationToken = default);
        Task<WorkloadCategory?> GetCurrentForFacultyAsync(Faculty faculty, DateTime asOf, CancellationToken cancellationToken = default);
    }
}
