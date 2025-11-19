using System.Text.Json.Serialization;
using AdminHubApi.Dtos.Common;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdExamDto
    {
        [JsonPropertyName("student_id")]
        public Guid StudentId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("course")]
        public string Course { get; set; } = string.Empty;

        [JsonPropertyName("course_code")]
        public string CourseCode { get; set; } = string.Empty;

        [JsonPropertyName("exam_date")]
        public DateTime ExamDate { get; set; }

        [JsonPropertyName("exam_time")]
        public int ExamTime { get; set; }

        [JsonPropertyName("exam_duration")]
        public int ExamDuration { get; set; }

        [JsonPropertyName("exam_score")]
        public int ExamScore { get; set; }
    }

    public class AntdExamResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("student_id")]
        public Guid StudentId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("course")]
        public string Course { get; set; } = string.Empty;

        [JsonPropertyName("course_code")]
        public string CourseCode { get; set; } = string.Empty;

        [JsonPropertyName("exam_date")]
        public DateTime ExamDate { get; set; }

        [JsonPropertyName("exam_time")]
        public int ExamTime { get; set; }

        [JsonPropertyName("exam_duration")]
        public int ExamDuration { get; set; }

        [JsonPropertyName("exam_score")]
        public int ExamScore { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class AntdExamQueryParams
    {
        public string? Course { get; set; }
        public string? CourseCode { get; set; }
        public Guid? StudentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class AntdExamListResponse
    {
        [JsonPropertyName("data")]
        public List<AntdExamResponseDto> Data { get; set; } = new();

        [JsonPropertyName("meta")]
        public PaginationMeta Meta { get; set; } = new();
    }

    public class AntdExamResponse
    {
        [JsonPropertyName("data")]
        public AntdExamResponseDto Data { get; set; } = new();
    }

    public class AntdExamCreateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdExamResponseDto Data { get; set; } = new();
    }

    public class AntdExamUpdateResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public AntdExamResponseDto Data { get; set; } = new();
    }

    public class AntdExamDeleteResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
