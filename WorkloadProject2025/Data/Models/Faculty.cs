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
    }
}
