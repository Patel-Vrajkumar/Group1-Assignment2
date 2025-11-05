namespace WorkloadProject2025.Data.Models;

public class InstructorAvailability
{
 public int Id { get; set; }
 public string FacultyEmail { get; set; } = string.Empty;
 public Faculty? Faculty { get; set; }

 public DayOfWeek DayOfWeek { get; set; }
 public TimeOnly StartTime { get; set; }
 public TimeOnly EndTime { get; set; }
}
