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

        // Employment category/role to support filtering and calculation rules
        public EmploymentCategory EmploymentCategory { get; set; } = EmploymentCategory.FullTime;

        // Optional notes/comments field
        public string? Notes { get; set; }

        // Convenience flag for active faculty
        public bool IsActive { get; set; } = true;
    }

    public enum EmploymentCategory
    {
        Unknown = 0,
        FullTime = 1,
        PartTime = 2,
        Adjunct = 3,
        Visiting = 4,
        Contract = 5
    }
}
