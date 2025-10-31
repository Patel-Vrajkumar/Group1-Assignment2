namespace WorkloadProject2025.Data.Models
{
    
    public class FacultyWorkLoad
    {
        public int Id { get; set; }

        public string FacultyEmail { get; set; } = string.Empty;
        public Faculty? Faculty { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public int TermId { get; set; }
        public Term? Term { get; set; }

        public int? WorkloadCategoryId { get; set; }
        public WorkloadCategory? WorkloadCategory { get; set; }

        public Workload Workload { get; set; } // enum: Lecture, Lab, etc.
        public decimal HoursAssigned { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAssigned { get; set; } = DateTime.Now;



    }

    public enum Workload
    {
        Course_Lecture,
        Course_Lab,
        Coordinator,
        Project
    }
}
