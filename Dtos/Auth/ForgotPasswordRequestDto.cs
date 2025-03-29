using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Auth;

public class ForgotPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}