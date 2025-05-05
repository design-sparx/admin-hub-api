using System.ComponentModel.DataAnnotations;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Projects;

public class CreateProjectDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = String.Empty;

    [StringLength(500)] public string Description { get; set; } = String.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
    
    [Required]
    public DateTime StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    [Required]
    public string OwnerId { get; set; }
}