using AdminHubApi.Enums.Mantine;
using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Entities.Mantine
{
    public class ChatMessages
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ChatId { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public MessageType MessageType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}