using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public interface ITermService
    {
        Task<Term> AddAsync(Term intake, CancellationToken cancellationToken = default);
        Task<List<Term>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Term?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        //Task<Intake?> GetByNameAsync(string IntakeName, CancellationToken cancellationToken = default);
    }
}
