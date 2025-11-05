namespace WorkloadProject2025.Data.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string IntakeName { get; set; } = string.Empty;
        public string Name => IntakeName; // Alias for compatibility
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Year => StartDate?.Year ?? DateTime.Now.Year;
    }
}
