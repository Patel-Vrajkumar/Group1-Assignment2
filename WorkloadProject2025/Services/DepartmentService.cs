using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            if (department == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(department.Name))
                throw new Exception("Department must have a name");

            _context.Departments.Add(department);
            await _context.SaveChangesAsync(cancellationToken);

            return department;
        }

        public Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Departments.ToListAsync(cancellationToken);
        }

        public Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }
    }
}
