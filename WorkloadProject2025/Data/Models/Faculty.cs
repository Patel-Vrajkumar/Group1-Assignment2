using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models
{
    public enum EmploymentStatus
    {
        Active,
        OnLeave,
        Retired,
        Inactive
    }

    public class Faculty
    {
        [Key]
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Optional department association
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Employment metadata
        public DateTime DateHired { get; set; } = DateTime.Today;
        public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;

        // Convenience property
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
