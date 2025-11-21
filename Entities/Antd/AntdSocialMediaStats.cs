using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdSocialMediaStats
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public int Followers { get; set; }

        public int Following { get; set; }

        public int Posts { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public decimal EngagementRate { get; set; }

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
