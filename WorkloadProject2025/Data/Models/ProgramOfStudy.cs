namespace WorkloadProject2025.Data.Models
{
    public class ProgramOfStudy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<Course> Courses { get; set; } = new();
    }
}
