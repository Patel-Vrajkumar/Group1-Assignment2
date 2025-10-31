namespace WorkloadProject2025.Data.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string IntakeName { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
