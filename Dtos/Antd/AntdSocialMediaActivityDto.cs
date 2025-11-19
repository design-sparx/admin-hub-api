using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdSocialMediaActivityDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("activity_type")]
        public string ActivityType { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonPropertyName("post_content")]
        public string PostContent { get; set; } = string.Empty;

        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;

        [JsonPropertyName("user_location")]
        public string UserLocation { get; set; } = string.Empty;

        [JsonPropertyName("user_age")]
        public int UserAge { get; set; }

        [JsonPropertyName("user_gender")]
        public string UserGender { get; set; } = string.Empty;

        [JsonPropertyName("user_interests")]
        public string UserInterests { get; set; } = string.Empty;

        [JsonPropertyName("user_friends_count")]
        public int UserFriendsCount { get; set; }
    }

    public class AntdSocialMediaActivityQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Platform { get; set; }
        public string? ActivityType { get; set; }
        public string? UserGender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public DateTime? TimestampFrom { get; set; }
        public DateTime? TimestampTo { get; set; }
        public string SortBy { get; set; } = "timestamp";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdSocialMediaActivityResponse : ApiResponse<AntdSocialMediaActivityDto> { }
    public class AntdSocialMediaActivityListResponse : ApiResponse<List<AntdSocialMediaActivityDto>> { }
    public class AntdSocialMediaActivityCreateResponse : ApiResponse<AntdSocialMediaActivityDto> { }
    public class AntdSocialMediaActivityUpdateResponse : ApiResponse<AntdSocialMediaActivityDto> { }
    public class AntdSocialMediaActivityDeleteResponse : ApiResponse<object> { }
}
