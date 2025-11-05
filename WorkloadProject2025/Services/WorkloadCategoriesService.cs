using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class WorkloadCategoriesService : IWorkloadCategoriesService
    {
        private readonly ApplicationDbContext _context;
        public WorkloadCategoriesService(ApplicationDbContext context) { _context = context; }

        public Task<List<WorkloadCategory>> GetAllAsync(CancellationToken cancellationToken = default)
            => _context.WorkloadCategories.OrderBy(c => c.Name).ToListAsync(cancellationToken);

        public Task<WorkloadCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => _context.WorkloadCategories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task<WorkloadCategory> AddAsync(WorkloadCategory category, CancellationToken cancellationToken = default)
        {
            if (category == null)
                throw new ArgumentNullException();
            if (category.MiniumHours == 0)
            {
                throw new Exception("Minimum Hours Must have an Amount");
            }
            if (category.MaximumHours < category.MiniumHours)
            {
                throw new Exception("Maximum Hours Must have an Amount higher than Minimum Hours");
            }
            if (category.EndDate <= category.StartDate)
            {
                throw new Exception("The End Date must be After the Start Date");
            }
            _context.WorkloadCategories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task<WorkloadCategory> UpdateAsync(WorkloadCategory category, CancellationToken cancellationToken = default)
        {
            _context.WorkloadCategories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.WorkloadCategories.FindAsync([id], cancellationToken);
            if (entity == null) return false;
            _context.WorkloadCategories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<WorkloadCategory?> GetCurrentForAsync(EmploymentCategory? employmentCategory, DateTime asOf, CancellationToken cancellationToken = default)
        {
            var q = _context.WorkloadCategories.AsQueryable();
            if (employmentCategory.HasValue)
            {
                q = q.Where(w => w.AppliesToEmploymentCategory == employmentCategory);
            }
            q = q.Where(w => w.StartDate <= asOf && (w.EndDate == null || w.EndDate >= asOf));
            return await q.OrderByDescending(w => w.StartDate).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<WorkloadCategory?> GetCurrentForFacultyAsync(Faculty faculty, DateTime asOf, CancellationToken cancellationToken = default)
        {
            return GetCurrentForAsync(faculty.EmploymentCategory, asOf, cancellationToken);
        }
    }
}
