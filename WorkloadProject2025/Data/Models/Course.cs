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
    }
}
