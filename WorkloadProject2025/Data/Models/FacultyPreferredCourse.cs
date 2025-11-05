namespace WorkloadProject2025.Data.Models;

public class FacultyPreferredCourse
{
 public int Id { get; set; }
 public string FacultyEmail { get; set; } = string.Empty;
 public Faculty? Faculty { get; set; }

 public int CourseId { get; set; }
 public Course? Course { get; set; }
}
