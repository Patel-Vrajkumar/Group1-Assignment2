using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public interface IWorkloadCategoriesService
    {
        Task<WorkloadCategory> AddAsync(WorkloadCategory workloadcategory, CancellationToken cancellationToken = default);
        Task<List<WorkloadCategory>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<WorkloadCategory?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
    }
}
