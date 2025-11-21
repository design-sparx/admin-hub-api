using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdFaqDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [JsonPropertyName("answer")]
        public string Answer { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("date_created")]
        public string DateCreated { get; set; } = string.Empty;

        [JsonPropertyName("is_featured")]
        public bool IsFeatured { get; set; }

        [JsonPropertyName("views")]
        public int Views { get; set; }

        [JsonPropertyName("tags")]
        public string Tags { get; set; } = string.Empty;

        [JsonPropertyName("rating")]
        public decimal Rating { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;
    }

    public class AntdFaqQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Category { get; set; }
        public bool? IsFeatured { get; set; }
        public decimal? MinRating { get; set; }
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; } = "views";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdFaqStatisticsDto
    {
        [JsonPropertyName("total_faqs")]
        public int TotalFaqs { get; set; }

        [JsonPropertyName("total_views")]
        public int TotalViews { get; set; }

        [JsonPropertyName("average_rating")]
        public decimal AverageRating { get; set; }

        [JsonPropertyName("featured_count")]
        public int FeaturedCount { get; set; }

        [JsonPropertyName("faqs_by_category")]
        public Dictionary<string, int> FaqsByCategory { get; set; } = new();
    }

    public class AntdFaqResponse : ApiResponse<AntdFaqDto>
    {
    }

    public class AntdFaqListResponse : ApiResponse<List<AntdFaqDto>>
    {
    }

    public class AntdFaqStatisticsResponse : ApiResponse<AntdFaqStatisticsDto>
    {
    }

    public class AntdFaqCreateResponse : ApiResponse<AntdFaqDto>
    {
    }

    public class AntdFaqUpdateResponse : ApiResponse<AntdFaqDto>
    {
    }

    public class AntdFaqDeleteResponse : ApiResponse<object>
    {
    }
}
