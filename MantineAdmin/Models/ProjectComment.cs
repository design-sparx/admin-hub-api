using System.ComponentModel.DataAnnotations.Schema;

namespace MantineAdmin;

[Table("ProjectComments")]
public class ProjectComment
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
}