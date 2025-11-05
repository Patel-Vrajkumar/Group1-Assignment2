using System.ComponentModel.DataAnnotations;

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
        
        // Audit fields
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        
        // Favorites/Pins
        public bool IsPinned { get; set; } = false;
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
