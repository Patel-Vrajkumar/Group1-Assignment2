using System.ComponentModel.DataAnnotations;

namespace WorkloadProject2025.Data.Models
{
    public class WorkloadCategory
    {
        [Key]
        public int Id { get; set; }

        public int MinimumHours { get; set; }

        public int MaximumHours { get; set; }

        public DateTime? StartDate { get; set; }
        // Nullable EndDate then we know it is the current one
        public DateTime? EndDate { get; set; }

    }
}
