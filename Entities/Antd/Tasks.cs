using AdminHubApi.Enums.Antd;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities.Antd
{
    public class AntdTask
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public AntdTaskPriority Priority { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string AssignedTo { get; set; } = string.Empty;

        [Required]
        public AntdTaskStatus Status { get; set; }

        [MaxLength(2000)]
        public string Notes { get; set; } = string.Empty;

        [Required]
        public AntdTaskCategory Category { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Duration { get; set; }

        public DateTime? CompletedDate { get; set; }

        [Required]
        public AntdTaskColor Color { get; set; }

        [MaxLength(50)]
        public string FavoriteColor { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
