using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdSocialMediaActivity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Author { get; set; } = string.Empty;

        [MaxLength(100)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ActivityType { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        public string PostContent { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Platform { get; set; } = string.Empty;

        [MaxLength(255)]
        public string UserLocation { get; set; } = string.Empty;

        public int UserAge { get; set; }

        [MaxLength(50)]
        public string UserGender { get; set; } = string.Empty;

        public string UserInterests { get; set; } = string.Empty;

        public int UserFriendsCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
