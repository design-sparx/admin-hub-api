using System.ComponentModel.DataAnnotations;

namespace AdminHubApi.Dtos.UserManagement;

public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    public List<string> Roles { get; set; } = new List<string>();
    public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
}