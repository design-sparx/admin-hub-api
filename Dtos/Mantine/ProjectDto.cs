namespace AdminHubApi.Dtos.Mantine
{
    public class ProjectDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Assignee { get; set; } = string.Empty;
    }

    public class ProjectQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public string? State { get; set; }
        public string? Assignee { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public string SortBy { get; set; } = "startDate";
        public string SortOrder { get; set; } = "desc";
    }
}