using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi;

[Table("AppUserProjects")]
public class AppUserProject
{
    public string AppUserId { get; set; }
    public int ProjectId { get; set; }
    public AppUser AppUser { get; set; }
    public Project Project { get; set; }
}