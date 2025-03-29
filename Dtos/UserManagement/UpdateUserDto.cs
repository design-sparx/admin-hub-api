using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.UserManagement;

public class UpdateUserDto
{
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    public List<string>? Roles { get; set; }
    public List<ClaimDto>? Claims { get; set; }
    public bool? LockoutEnabled { get; set; }
}