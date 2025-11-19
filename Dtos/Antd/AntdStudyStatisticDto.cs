using System.Text.Json.Serialization;
using AdminHubApi.Dtos.Common;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdStudyStatisticDto
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;
    }

    public class AntdStudyStatisticResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class AntdStudyStatisticQueryParams
    {
        public string? Category { get; set; }
        public string? Month { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class AntdStudyStatisticListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdStudyStatisticResponseDto> Data { get; set; } = new();

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; } = new();
    }

    public class AntdStudyStatisticResponse
    {
        [JsonPropertyName("data")]
        public AntdStudyStatisticResponseDto Data { get; set; } = new();
    }

    public class AntdStudyStatisticCreateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdStudyStatisticResponseDto Data { get; set; } = new();
    }

    public class AntdStudyStatisticUpdateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdStudyStatisticResponseDto Data { get; set; } = new();
    }

    public class AntdStudyStatisticDeleteResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
