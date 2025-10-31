using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class WorkloadCategoriesService : IWorkloadCategoriesService
    {
        ApplicationDbContext _context;
        public WorkloadCategoriesService(ApplicationDbContext db)
        {
            _context = db;
        }
        public async Task<WorkloadCategory> AddAsync(WorkloadCategory workloadcategory, CancellationToken cancellationToken = default)
        {
            if (workloadcategory == null)
                throw new ArgumentNullException();
            if (workloadcategory.MiniumHours == 0)
            {
                throw new Exception("Minimum Hours Must have an Amount");
            }
            if (workloadcategory.MaximumHours < workloadcategory.MiniumHours) 
            {
                throw new Exception("Maximum Hours Must have an Amount higher than Minimum Hours");
            }
            if (workloadcategory.EndDate <= workloadcategory.StartDate) 
            {
                throw new Exception("The End Date must be After the Start Date");
            }
            _context.WorkloadCategories.Add(workloadcategory);
            await _context.SaveChangesAsync();
            return workloadcategory;
        }

        public Task<WorkloadCategory?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.WorkloadCategories.FirstOrDefaultAsync(WorkloadCategory => WorkloadCategory.Id == id);
        }



        Task<List<WorkloadCategory>> IWorkloadCategoriesService.GetAllAsync(CancellationToken cancellationToken)
        {
            return _context.WorkloadCategories.ToListAsync();
        }
    }
}
