using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<WorkloadCategory> WorkloadCategories { get; set; }
        public DbSet<ProgramOfStudy> ProgramsOfStudy { get; set; }
        public DbSet<FacultyWorkLoad> FacultyWorkLoads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for HoursAssigned to prevent truncation
            modelBuilder.Entity<FacultyWorkLoad>()
                .Property(f => f.HoursAssigned)
                .HasPrecision(18, 2);
        }
    }
}
