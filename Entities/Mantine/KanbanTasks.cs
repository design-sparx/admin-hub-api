using System.ComponentModel.DataAnnotations;
using TaskStatus = AdminHubApi.Enums.Mantine.TaskStatus;

namespace AdminHubApi.Entities.Mantine
{
    public class KanbanTasks
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public TaskStatus Status { get; set; }

        public int Comments { get; set; } = 0;

        public int Users { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}