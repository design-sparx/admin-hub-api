using AdminHubApi.Enums.Mantine;
using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class FileActivities
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid FileId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public FileAction Action { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}