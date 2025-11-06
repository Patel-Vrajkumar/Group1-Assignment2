namespace WorkloadProject2025.Data.Models;

public class CourseSchedule
{
    public int Id { get; set; }
    
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    
    public int TermId { get; set; }
    public Term? Term { get; set; }
    
    // Schedule details
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Room { get; set; }
    
    // Enrollment details
    public int EnrolledStudents { get; set; }
    public int MaxCapacity { get; set; } = 30;
    
    // Instructor assignment
    public string? InstructorEmail { get; set; }
    public Faculty? Instructor { get; set; }
    
    // Course type
    public CourseType CourseType { get; set; } = CourseType.Lecture;
}

public enum CourseType
{
    Lecture,
    Lab,
    Tutorial,
    Seminar
}
