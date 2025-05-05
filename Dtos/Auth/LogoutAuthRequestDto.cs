using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Auth;

public class LogoutAuthRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}