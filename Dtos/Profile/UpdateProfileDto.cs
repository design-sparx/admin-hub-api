using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Profile;

public class UpdateProfileDto
{
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
}