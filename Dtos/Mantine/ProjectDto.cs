using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Enums.Mantine;
using System.Text.Json.Serialization;

namespace AdminHubApi.Dtos.Mantine
{
    public class ProjectDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; } = string.Empty;

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } = string.Empty;

        public ProjectState State { get; set; }
        public string Assignee { get; set; } = string.Empty;
    }

    public class ProjectQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public ProjectState? State { get; set; }
        public string? Assignee { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public string SortBy { get; set; } = "startDate";
        public string SortOrder { get; set; } = "desc";
    }

    public class ProjectResponse : ApiResponse<ProjectDto>
    {
    }

    public class ProjectListResponse : ApiResponse<List<ProjectDto>>
    {
    }

    public class ProjectCreateResponse : ApiResponse<ProjectDto>
    {
    }

    public class ProjectUpdateResponse : ApiResponse<ProjectDto>
    {
    }

    public class ProjectDeleteResponse : ApiResponse<object>
    {
    }
}