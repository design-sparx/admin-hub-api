using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Auth;

public class RefreshTokenRequestDto
{
    [Required]
    public string Token { get; set; }
}