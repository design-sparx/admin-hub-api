using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Auth;

public class AuthRequestDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}