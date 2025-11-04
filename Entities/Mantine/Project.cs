using AdminHubApi.Enums.Mantine;
using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class Projects
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public ProjectState State { get; set; }

        [Required]
        [MaxLength(255)]
        public string Assignee { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}