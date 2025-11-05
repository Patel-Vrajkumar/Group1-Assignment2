namespace WorkloadProject2025.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }

        public int ProgramOfStudyId { get; set; }
        public ProgramOfStudy? ProgramOfStudy { get; set; }

        // Optional linkage to an academic term for term-specific courses
        public int? TermId { get; set; }
        public Term? Term { get; set; }

        // Planning and scheduling data
        public int? Enrollment { get; set; }
        public string? MeetingDays { get; set; } // e.g., "Mon/Wed" or pattern code
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Room { get; set; }
        public string? BlockSlot { get; set; } // e.g., "Block A", "Block2"
    }
}
