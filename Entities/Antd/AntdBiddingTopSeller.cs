using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdBiddingTopSeller
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Artist { get; set; } = string.Empty;

        public int Volume { get; set; }

        public int Status { get; set; }

        public int OwnersCount { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public int Edition { get; set; }

        public decimal Price { get; set; }

        [MaxLength(255)]
        public string Owner { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Collection { get; set; } = string.Empty;

        public bool Verified { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
