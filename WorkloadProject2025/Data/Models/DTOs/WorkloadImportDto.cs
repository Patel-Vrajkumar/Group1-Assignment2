namespace WorkloadProject2025.Data.Models.DTOs
{
    public class WorkloadImportDto
    {
        public string FacultyEmail { get; set; } = string.Empty;
      public string? CourseName { get; set; }
        public string TermName { get; set; } = string.Empty;
        public string WorkloadType { get; set; } = string.Empty;
    public decimal HoursAssigned { get; set; }
        public string? Description { get; set; }
        public string? Annotations { get; set; }
        
        // Validation results
        public List<string> ValidationErrors { get; set; } = new();
        public bool IsValid => ValidationErrors.Count == 0;
    }
    
    public class WorkloadCloneRequest
    {
  public int SourceTermId { get; set; }
  public int TargetTermId { get; set; }
        public List<string>? FacultyEmails { get; set; } // null = all faculty
        public List<int>? DepartmentIds { get; set; } // null = all departments
        public bool IncludeAnnotations { get; set; } = true;
    }
    
    public class BulkUpdateRequest
    {
        public List<int> WorkloadIds { get; set; } = new();
        public decimal? HoursAssigned { get; set; }
        public WorkloadStatus? Status { get; set; }
     public string? Annotations { get; set; }
     public bool AppendAnnotations { get; set; } = true; // If true, adds to existing; if false, replaces
    }
}
