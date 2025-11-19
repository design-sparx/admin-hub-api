using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdCourseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("instructor_name")]
        public string InstructorName { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("credit_hours")]
        public int CreditHours { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; } = string.Empty;

        [JsonPropertyName("prerequisites")]
        public string Prerequisites { get; set; } = string.Empty;

        [JsonPropertyName("course_location")]
        public string CourseLocation { get; set; } = string.Empty;

        [JsonPropertyName("total_lessons")]
        public int TotalLessons { get; set; }

        [JsonPropertyName("current_lessons")]
        public int CurrentLessons { get; set; }

        [JsonPropertyName("favorite_color")]
        public string FavoriteColor { get; set; } = string.Empty;
    }

    public class AntdCourseQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Department { get; set; }
        public string? InstructorName { get; set; }
        public int? MinCreditHours { get; set; }
        public int? MaxCreditHours { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public string SortBy { get; set; } = "startdate";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdCourseResponse : ApiResponse<AntdCourseDto> { }
    public class AntdCourseListResponse : ApiResponse<List<AntdCourseDto>> { }
    public class AntdCourseCreateResponse : ApiResponse<AntdCourseDto> { }
    public class AntdCourseUpdateResponse : ApiResponse<AntdCourseDto> { }
    public class AntdCourseDeleteResponse : ApiResponse<object> { }
}
