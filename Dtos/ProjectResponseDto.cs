namespace AdminHubApi.Dtos;

public class ProjectResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } = String.Empty;
    public string Status { get; set; } = String.Empty;
}