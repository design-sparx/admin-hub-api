using AdminHubApi.Enums.Mantine;
using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class Chats
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ChatType Type { get; set; }

        public int ParticipantCount { get; set; } = 0;

        [MaxLength(500)]
        public string? LastMessage { get; set; }

        public DateTime? LastMessageAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}