using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Profile;

public class ChangePasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string CurrentPassword { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }
    
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}