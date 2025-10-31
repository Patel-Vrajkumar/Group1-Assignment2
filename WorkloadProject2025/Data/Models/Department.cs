namespace WorkloadProject2025.Data.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int SchoolId { get; set; }
    public School? School { get; set; }

    public List<ProgramOfStudy> ProgramsOfStudy { get; set; } = new();

}
