using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Mantine
{
    public class Traffic
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Source { get; set; } = string.Empty;

        public int Visitors { get; set; }

        public int Pageviews { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal BounceRate { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal AvgSessionDuration { get; set; }

        public DateTime Date { get; set; }
    }
}