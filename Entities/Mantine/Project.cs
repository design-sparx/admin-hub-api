using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class Project
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Assignee { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class ProjectState
    {
        public const string Pending = "Pending";
        public const string InProgress = "In Progress";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
    }
}