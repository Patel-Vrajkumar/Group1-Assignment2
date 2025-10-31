using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public interface IFacultyService
    {
        Task<Faculty> AddAsync(Faculty faculty, CancellationToken cancellationToken = default);
        Task<List<Faculty>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Faculty?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    }
}
