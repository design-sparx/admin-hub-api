using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.UserManagement;

public class ResetPasswordDto
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }
}