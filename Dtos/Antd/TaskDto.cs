using AdminHubApi.Enums.Antd;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class TaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AntdTaskPriority Priority { get; set; }

        [JsonPropertyName("due_date")]
        public string DueDate { get; set; } = string.Empty;

        [JsonPropertyName("assigned_to")]
        public string AssignedTo { get; set; } = string.Empty;

        public AntdTaskStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public AntdTaskCategory Category { get; set; }
        public decimal Duration { get; set; }

        [JsonPropertyName("completed_date")]
        public string CompletedDate { get; set; } = string.Empty;

        public AntdTaskColor Color { get; set; }
    }

    public class TaskQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public AntdTaskStatus? Status { get; set; }
        public AntdTaskPriority? Priority { get; set; }
        public AntdTaskCategory? Category { get; set; }
        public AntdTaskColor? Color { get; set; }

        [JsonPropertyName("assigned_to")]
        public string AssignedTo { get; set; } = string.Empty;

        [JsonPropertyName("due_date_from")]
        public DateTime? DueDateFrom { get; set; }

        [JsonPropertyName("due_date_to")]
        public DateTime? DueDateTo { get; set; }

        [JsonPropertyName("min_duration")]
        public decimal? MinDuration { get; set; }

        [JsonPropertyName("max_duration")]
        public decimal? MaxDuration { get; set; }

        [JsonPropertyName("sort_by")]
        public string SortBy { get; set; } = "due_date";

        [JsonPropertyName("sort_order")]
        public string SortOrder { get; set; } = "desc";
    }
}
