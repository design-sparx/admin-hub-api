using TaskStatus = AdminHubApi.Enums.Mantine.TaskStatus;

namespace AdminHubApi.Dtos.Mantine
{
    public class KanbanTaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public int Comments { get; set; }
        public int Users { get; set; }
    }

    public class KanbanTaskQueryParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
        public TaskStatus? Status { get; set; }
        public string? Title { get; set; }
    }
}