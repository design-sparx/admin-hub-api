using AdminHubApi.Enums.Mantine;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Mantine
{
    public class FileDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        [JsonPropertyName("folder_id")]
        public string? FolderId { get; set; }

        [JsonPropertyName("owner_id")]
        public string OwnerId { get; set; } = string.Empty;
    }

    public class FolderDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        [JsonPropertyName("parent_id")]
        public string? ParentId { get; set; }

        [JsonPropertyName("owner_id")]
        public string OwnerId { get; set; } = string.Empty;
    }

    public class FileActivityDto
    {
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("file_id")]
        public string FileId { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;

        public FileAction Action { get; set; }
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = string.Empty;
    }

    public class FileQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;

        [JsonPropertyName("folder_id")]
        public string? FolderId { get; set; }

        [JsonPropertyName("file_type")]
        public string? FileType { get; set; }

        [JsonPropertyName("owner_id")]
        public string? OwnerId { get; set; }
    }

    public class FolderQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;

        [JsonPropertyName("parent_id")]
        public string? ParentId { get; set; }

        [JsonPropertyName("owner_id")]
        public string? OwnerId { get; set; }
    }
}