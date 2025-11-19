using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdSocialMediaStatsDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("followers")]
        public int Followers { get; set; }

        [JsonPropertyName("following")]
        public int Following { get; set; }

        [JsonPropertyName("posts")]
        public int Posts { get; set; }

        [JsonPropertyName("likes")]
        public int Likes { get; set; }

        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        [JsonPropertyName("engagement_rate")]
        public decimal EngagementRate { get; set; }
    }

    public class AntdSocialMediaStatsQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Title { get; set; }
        public string SortBy { get; set; } = "followers";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdSocialMediaStatsResponse : ApiResponse<AntdSocialMediaStatsDto> { }
    public class AntdSocialMediaStatsListResponse : ApiResponse<List<AntdSocialMediaStatsDto>> { }
    public class AntdSocialMediaStatsCreateResponse : ApiResponse<AntdSocialMediaStatsDto> { }
    public class AntdSocialMediaStatsUpdateResponse : ApiResponse<AntdSocialMediaStatsDto> { }
    public class AntdSocialMediaStatsDeleteResponse : ApiResponse<object> { }
}
