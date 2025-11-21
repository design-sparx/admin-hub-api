using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdScheduledPost
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime ScheduledDate { get; set; }

        public int ScheduledTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string Author { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public int SharesCount { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Location { get; set; } = string.Empty;

        public string Hashtags { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Platform { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
