using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdScheduledPostDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("tags")]
        public string Tags { get; set; } = string.Empty;

        [JsonPropertyName("likes_count")]
        public int LikesCount { get; set; }

        [JsonPropertyName("comments_count")]
        public int CommentsCount { get; set; }

        [JsonPropertyName("shares_count")]
        public int SharesCount { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("link")]
        public string Link { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("hashtags")]
        public string Hashtags { get; set; } = string.Empty;

        [JsonPropertyName("platform")]
        public string Platform { get; set; } = string.Empty;
    }

    public class AntdScheduledPostQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Platform { get; set; }
        public string? Category { get; set; }
        public string? Author { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string SortBy { get; set; } = "date";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdScheduledPostResponse : ApiResponse<AntdScheduledPostDto> { }
    public class AntdScheduledPostListResponse : ApiResponse<List<AntdScheduledPostDto>> { }
    public class AntdScheduledPostCreateResponse : ApiResponse<AntdScheduledPostDto> { }
    public class AntdScheduledPostUpdateResponse : ApiResponse<AntdScheduledPostDto> { }
    public class AntdScheduledPostDeleteResponse : ApiResponse<object> { }
}
