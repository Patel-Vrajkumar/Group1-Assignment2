namespace WorkloadProject2025.Data.Models
{
    
    public class FacultyWorkLoad
    {
        public int Id { get; set; }

        public string FacultyEmail { get; set; } = string.Empty;
        public Faculty? Faculty { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        // Program for cross-department/multi-program tracking
        public int? ProgramOfStudyId { get; set; }
        public ProgramOfStudy? ProgramOfStudy { get; set; }

        public int TermId { get; set; }
        public Term? Term { get; set; }

        public int? WorkloadCategoryId { get; set; }
        public WorkloadCategory? WorkloadCategory { get; set; }

        public Workload Workload { get; set; } // enum: Lecture, Lab, etc.
        public decimal HoursAssigned { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAssigned { get; set; } = DateTime.Now;

        // Notes/comments per workload item
        public string? Notes { get; set; }

        // Co-teaching support
        public bool IsPrimaryInstructor { get; set; } = true;
        // Optional percentage share between0 and100
        public decimal? PercentShare { get; set; }

        // Coverage tracking: this workload covers another instructor's class
        public bool IsCoverage { get; set; }
        public string? CoveredForFacultyEmail { get; set; }

        // HR processing flags and date tracking
        public bool HRProcessed { get; set; }
        public DateTime? HRProcessedDate { get; set; }
        public string? HRNotes { get; set; }
    }

    public enum Workload
    {
        // Teaching
        Course_Lecture =0,
        Course_Lab =1,
        // Legacy existing entries kept for compatibility
        Coordinator =2,
        Project =3,

        // Non-teaching / release / admin
        Coordinator_Release =4,
        Research_Release =5,
        Chair_Release =6,
        Admin_Duties =7,
        Committee_Work =8,
        Professional_Development =9,
        Sabbatical =10,
        Sick_Leave =11,
        Special_Assignment =12,
        Other =13
    }
}
