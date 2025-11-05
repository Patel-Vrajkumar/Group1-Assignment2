namespace WorkloadProject2025.Data.Models;

public class CourseAssignment
{
 public int Id { get; set; }

 public string InstructorEmail { get; set; } = string.Empty;
 public Faculty? Instructor { get; set; }

 public int CourseId { get; set; }
 public Course? Course { get; set; }

 public int TermId { get; set; }
 public Term? Term { get; set; }

 public TeachingRole Role { get; set; } = TeachingRole.Primary;

 public decimal AssignmentPercentage { get; set; } =100m; //100% by default

 public bool IsActive { get; set; } = true;

 public string? Notes { get; set; }
}

public enum TeachingRole
{
 Primary,
 CoInstructor,
 TeachingAssistant
}
