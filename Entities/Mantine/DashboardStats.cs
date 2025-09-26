using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class DashboardStats
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Value { get; set; } = string.Empty;

        public int Diff { get; set; }

        [MaxLength(20)]
        public string? Period { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}