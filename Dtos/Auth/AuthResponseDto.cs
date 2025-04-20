namespace AdminHubApi.Dtos.Auth;

public class AuthResponseDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    
    public UserDto User { get; set; }
    public IList<string> Roles { get; set; }
}