using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdCampaignAd
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string AdSource { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string AdCampaign { get; set; } = string.Empty;

        [MaxLength(255)]
        public string AdGroup { get; set; } = string.Empty;

        [MaxLength(100)]
        public string AdType { get; set; } = string.Empty;

        public int Impressions { get; set; }

        public int Clicks { get; set; }

        public int Conversions { get; set; }

        public decimal Cost { get; set; }

        public decimal ConversionRate { get; set; }

        public decimal Revenue { get; set; }

        public decimal Roi { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
