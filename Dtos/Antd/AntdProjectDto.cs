using AdminHubApi.Dtos.ApiResponse;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Antd
{
    public class AntdProjectDto
    {
        [JsonPropertyName("project_id")]
        public string ProjectId { get; set; } = string.Empty;

        [JsonPropertyName("project_name")]
        public string ProjectName { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } = string.Empty;

        [JsonPropertyName("budget")]
        public string Budget { get; set; } = string.Empty;

        [JsonPropertyName("project_manager")]
        public string ProjectManager { get; set; } = string.Empty;

        [JsonPropertyName("client_name")]
        public string ClientName { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("priority")]
        public string Priority { get; set; } = string.Empty;

        [JsonPropertyName("team_size")]
        public int TeamSize { get; set; }

        [JsonPropertyName("project_description")]
        public string ProjectDescription { get; set; } = string.Empty;

        [JsonPropertyName("project_location")]
        public string ProjectLocation { get; set; } = string.Empty;

        [JsonPropertyName("project_type")]
        public string ProjectType { get; set; } = string.Empty;

        [JsonPropertyName("project_category")]
        public string ProjectCategory { get; set; } = string.Empty;

        [JsonPropertyName("project_duration")]
        public decimal ProjectDuration { get; set; }
    }

    public class AntdProjectQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? ProjectManager { get; set; }
        public string? ClientName { get; set; }
        public string? ProjectType { get; set; }
        public string? ProjectCategory { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public string SortBy { get; set; } = "startDate";
        public string SortOrder { get; set; } = "desc";
    }

    public class AntdProjectResponse : ApiResponse<AntdProjectDto>
    {
    }

    public class AntdProjectListResponse : ApiResponse<List<AntdProjectDto>>
    {
    }

    public class AntdProjectCreateResponse : ApiResponse<AntdProjectDto>
    {
    }

    public class AntdProjectUpdateResponse : ApiResponse<AntdProjectDto>
    {
    }

    public class AntdProjectDeleteResponse : ApiResponse<object>
    {
    }
}
