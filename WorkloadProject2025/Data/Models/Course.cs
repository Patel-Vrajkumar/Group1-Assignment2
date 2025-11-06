namespace WorkloadProject2025.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }

        public int ProgramOfStudyId { get; set; }
        public ProgramOfStudy? ProgramOfStudy { get; set; }
        
        // Enhanced fields
        public bool IsPinned { get; set; } = false;
        public string? CourseCode { get; set; } // e.g., "CS101"
        
        // Schedule and enrollment fields for calendar view
        public string? Room { get; set; } // e.g., "B231"
        public TimeSpan? StartTime { get; set; } // e.g., 08:00
        public TimeSpan? EndTime { get; set; } // e.g., 09:00
        public DayOfWeek? DayOfWeek { get; set; }
        public int? EnrolledStudents { get; set; } // Number of students enrolled
        public int? MaxCapacity { get; set; } // Maximum class capacity
    }
}
