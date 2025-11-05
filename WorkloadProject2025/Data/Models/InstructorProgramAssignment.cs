namespace WorkloadProject2025.Data.Models;

public class InstructorProgramAssignment
{
 public int Id { get; set; }
 public string FacultyEmail { get; set; } = string.Empty;
 public Faculty? Faculty { get; set; }

 public int ProgramOfStudyId { get; set; }
 public ProgramOfStudy? ProgramOfStudy { get; set; }

 public int? MaxCourseLoadOverride { get; set; } // allow per-program overrides
}
