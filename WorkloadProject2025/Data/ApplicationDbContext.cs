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

        // New entities
        public DbSet<FacultyQualification> FacultyQualifications { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<InstructorAvailability> InstructorAvailabilities { get; set; }
        public DbSet<InstructorPreference> InstructorPreferences { get; set; }
        public DbSet<FacultyPreferredCourse> FacultyPreferredCourses { get; set; }
        public DbSet<InstructorProgramAssignment> InstructorProgramAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for HoursAssigned to prevent truncation
            modelBuilder.Entity<FacultyWorkLoad>()
                .Property(f => f.HoursAssigned)
                .HasPrecision(18, 2);
  
            // Configure precision for Faculty MaxTeachingHours
            modelBuilder.Entity<Faculty>()
                .Property(f => f.MaxTeachingHours)
                .HasPrecision(18, 2);

            // Relationships and keys
            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.Qualifications)
                .WithOne(q => q.Faculty!)
                .HasForeignKey(q => q.FacultyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Faculty>()
                .HasMany(f => f.CourseAssignments)
                .WithOne(ca => ca.Instructor!)
                .HasForeignKey(ca => ca.InstructorEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacultyPreferredCourse>()
                .HasOne(x => x.Faculty)
                .WithMany()
                .HasForeignKey(x => x.FacultyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacultyPreferredCourse>()
                .HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InstructorAvailability>()
                .HasOne(x => x.Faculty)
                .WithMany()
                .HasForeignKey(x => x.FacultyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InstructorPreference>()
                .HasOne(x => x.Faculty)
                .WithMany()
                .HasForeignKey(x => x.FacultyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InstructorProgramAssignment>()
                .HasOne(x => x.Faculty)
                .WithMany()
                .HasForeignKey(x => x.FacultyEmail)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InstructorProgramAssignment>()
                .HasOne(x => x.ProgramOfStudy)
                .WithMany()
                .HasForeignKey(x => x.ProgramOfStudyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
