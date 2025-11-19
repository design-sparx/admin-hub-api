using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Antd
{
    public class AntdProject
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string ProjectName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(100)]
        public string Budget { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string ProjectManager { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Priority { get; set; } = string.Empty;

        public int TeamSize { get; set; }

        public string ProjectDescription { get; set; } = string.Empty;

        [MaxLength(255)]
        public string ProjectLocation { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ProjectType { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ProjectCategory { get; set; } = string.Empty;

        public decimal ProjectDuration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
