using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public interface ICourseService
    {
        Task<Course> AddAsync(Course course, CancellationToken cancellationToken = default);
        Task<List<Course>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
