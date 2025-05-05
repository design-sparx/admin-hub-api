using System.ComponentModel.DataAnnotations.Schema;

namespace AdminHubApi.Entities;

public enum ProjectStatus
{
    Pending,
    Active,
    OnHold,
    Completed,
    Cancelled
}

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } = String.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public ApplicationUser Owner { get; set; }
}