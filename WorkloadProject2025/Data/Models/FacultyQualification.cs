using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models;

public class FacultyQualification
{
 public int Id { get; set; }

 [Required]
 public string FacultyEmail { get; set; } = string.Empty;
 public Faculty? Faculty { get; set; }

 [Required]
 [MaxLength(128)]
 public string Type { get; set; } = string.Empty; // Degree, Certification, Specialization

 [MaxLength(256)]
 public string? Title { get; set; } // e.g., MSc Computer Science, PMP

 public DateTime? AwardedOn { get; set; }
}
