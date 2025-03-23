namespace AdminHubApi.Dtos.Projects;

public class CreateProjectDto
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Status { get; set; } = String.Empty;
}