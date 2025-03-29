using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.UserManagement;

public class ChangePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }
    
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}