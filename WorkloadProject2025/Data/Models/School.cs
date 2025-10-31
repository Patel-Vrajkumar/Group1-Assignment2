namespace WorkloadProject2025.Data.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Department> Departments { get; set; } = new();

    }
}
