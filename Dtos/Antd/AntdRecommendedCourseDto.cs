using System.Text.Json.Serialization;
using AdminHubApi.Dtos.ApiResponse;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdRecommendedCourseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("instructor")]
        public string Instructor { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("course_language")]
        public string CourseLanguage { get; set; } = string.Empty;

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;

        [JsonPropertyName("lessons")]
        public int Lessons { get; set; }
    }

    public class AntdRecommendedCourseResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("instructor")]
        public string Instructor { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("course_language")]
        public string CourseLanguage { get; set; } = string.Empty;

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;

        [JsonPropertyName("lessons")]
        public int Lessons { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class AntdRecommendedCourseQueryParams
    {
        public string? Level { get; set; }
        public string? Category { get; set; }
        public string? Instructor { get; set; }
        public string? CourseLanguage { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class AntdRecommendedCourseListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdRecommendedCourseResponseDto> Data { get; set; } = new();

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; } = new();
    }

    public class AntdRecommendedCourseResponse
    {
        [JsonPropertyName("data")]
        public AntdRecommendedCourseResponseDto Data { get; set; } = new();
    }

    public class AntdRecommendedCourseCreateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdRecommendedCourseResponseDto Data { get; set; } = new();
    }

    public class AntdRecommendedCourseUpdateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdRecommendedCourseResponseDto Data { get; set; } = new();
    }

    public class AntdRecommendedCourseDeleteResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
