using AdminHubApi.Enums.Mantine;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Mantine
{
    public class ChatDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ChatType Type { get; set; }

        [JsonPropertyName("participant_count")]
        public int ParticipantCount { get; set; }

        [JsonPropertyName("last_message")]
        public string? LastMessage { get; set; }

        [JsonPropertyName("last_message_at")]
        public string? LastMessageAt { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = string.Empty;
    }

    public class ChatMessageDto
    {
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("chat_id")]
        public string ChatId { get; set; } = string.Empty;

        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("message_type")]
        public MessageType MessageType { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = string.Empty;
    }

    public class ChatQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public ChatType? Type { get; set; }
    }

    public class ChatMessageQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;

        [JsonPropertyName("chat_id")]
        public string? ChatId { get; set; }

        [JsonPropertyName("sender_id")]
        public string? SenderId { get; set; }

        [JsonPropertyName("message_type")]
        public MessageType? MessageType { get; set; }
    }
}