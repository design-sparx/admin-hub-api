using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Auth;

public class ResetPasswordRequestDto
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public string Token { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }
    
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; }
}