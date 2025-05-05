using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;

namespace AdminHubApi.Dtos.Projects;

public class ProjectResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public ProjectStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string OwnerId { get; set; }
    public UserDto Owner { get; set; }
    public string StatusText => Status.ToString();
    public int CompletionPercentage 
    { 
        get 
        {
            return Status switch
            {
                ProjectStatus.Completed => 100,
                ProjectStatus.Cancelled => 0,
                ProjectStatus.Pending => 0,
                ProjectStatus.OnHold => CalculateCompletionPercentage(),
                ProjectStatus.Active => CalculateCompletionPercentage(),
                _ => 0
            };
        }
    }
    
    private int CalculateCompletionPercentage()
    {
        // Simple calculation based on dates
        if (StartDate == null || DueDate == null) return 0;
        
        var totalDays = (DueDate.Value - StartDate.Value).TotalDays;
        if (totalDays <= 0) return 0;
        
        var elapsedDays = (DateTime.UtcNow - StartDate.Value).TotalDays;
        var percentage = (int)Math.Min(100, Math.Max(0, (elapsedDays / totalDays) * 100));
        
        return percentage;
    }
}