using System.Text.Json;

namespace WorkloadProject2025.Data.Models;

public class InstructorPreference
{
 public int Id { get; set; }
 public string FacultyEmail { get; set; } = string.Empty;
 public Faculty? Faculty { get; set; }

 public string? PreferredDeliveryMode { get; set; } // Online, In-Person, Hybrid
 public int? MaxTravelDistanceKm { get; set; }
 public string? PreferredCampus { get; set; }
}
