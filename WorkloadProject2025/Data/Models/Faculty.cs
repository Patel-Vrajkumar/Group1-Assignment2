using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkloadProject2025.Data.Models
{
    public class Faculty
    {
        [Key]
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        
        // Enhanced fields
        public EmploymentCategory EmploymentCategory { get; set; } = EmploymentCategory.FullTime;
        public string? Comments { get; set; }
        public decimal MaxTeachingHours { get; set; } = 12.0m; // Default max hours per semester

        // New fields for categorization & management
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public DateTime? HireDate { get; set; }

        // Course-load based limit (number of courses)
        public int MaxCourseLoad { get; set; } = 4; // default aligns with FullTime

        // Navigation properties
        public List<FacultyQualification> Qualifications { get; set; } = new();
        public List<CourseAssignment> CourseAssignments { get; set; } = new();

        // Audit fields
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        
        // Favorites/Pins
        public bool IsPinned { get; set; } = false;

        [NotMapped]
        public int CurrentWorkload => CourseAssignments?.Count(a => a.IsActive) ?? 0;
    }
    
    public enum EmploymentCategory
    {
        FullTime,
        PartTime,
        Adjunct,
        Visiting,
        Emeritus
    }
}
