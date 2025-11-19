using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdCommunityGroup
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Location { get; set; } = string.Empty;

        public int Size { get; set; }

        [MaxLength(255)]
        public string Leader { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        [MaxLength(20)]
        public string MeetingTime { get; set; } = string.Empty;

        public int MemberAgeRange { get; set; }

        public string MemberInterests { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
