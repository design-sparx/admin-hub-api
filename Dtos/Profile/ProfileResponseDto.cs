using AdminHubApi.Dtos.UserManagement;

namespace AdminHubApi.Dtos.Profile;

public class ProfileResponseDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public IList<string> Roles { get; set; }
    public IList<ClaimDto> Claims { get; set; }

}