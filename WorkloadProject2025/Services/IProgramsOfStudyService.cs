using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public interface IProgramsOfStudyService
    {
        Task<ProgramOfStudy> AddAsync(ProgramOfStudy program, CancellationToken cancellationToken = default);
        Task<List<ProgramOfStudy>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProgramOfStudy?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
