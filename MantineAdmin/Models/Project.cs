using System.ComponentModel.DataAnnotations.Schema;

namespace MantineAdmin;

[Table("Projects")]
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<ProjectComment> Comments { get; set; } = new List<ProjectComment>();
    public List<AppUserProject> AppUserProjects { get; set; } = new List<AppUserProject>();
}