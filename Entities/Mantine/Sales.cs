using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Mantine
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Source { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal RevenueAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string RevenueFormatted { get; set; } = string.Empty;

        [Column(TypeName = "decimal(5,2)")]
        public decimal Value { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}