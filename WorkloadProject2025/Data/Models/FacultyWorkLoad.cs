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

        // Enhanced fields
        public string? Annotations { get; set; } // Notes specific to this workload item
        public WorkloadStatus Status { get; set; } = WorkloadStatus.Draft;
        
        // Audit fields
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        
        // Approval workflow
        public DateTime? SubmittedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string? RejectionReason { get; set; }
        
        // Cloning tracking
        public int? ClonedFromId { get; set; }
        public int? SourceYear { get; set; }
    }

    public enum Workload
    {
        Course_Lecture,
        Course_Lab,
        Coordinator,
        Project
    }
    
    public enum WorkloadStatus
    {
        Draft,
        Pending,
        Approved,
        Rejected,
        Overloaded
    }
}
