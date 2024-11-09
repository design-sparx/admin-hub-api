using AdminHubApi.Dtos.ProjectComment;

namespace AdminHubApi.Dtos.Project;

public class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<ProjectCommentDto> Comments { get; set; }
}