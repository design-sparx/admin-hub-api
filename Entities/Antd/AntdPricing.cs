using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdPricing
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Plan { get; set; } = string.Empty;

        public decimal Monthly { get; set; }

        public decimal Annually { get; set; }

        [MaxLength(100)]
        public string SavingsCaption { get; set; } = string.Empty;

        // Store features as newline-separated string
        public string Features { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Color { get; set; } = string.Empty;

        public bool Preferred { get; set; }

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
