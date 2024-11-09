using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.Account;

public class LoginDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}