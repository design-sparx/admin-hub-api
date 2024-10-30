namespace MantineAdmin.Dtos.ProjectComment;

public class ProjectCommentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int? ProjectId { get; set; }
}