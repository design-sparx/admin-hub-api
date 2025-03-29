namespace AdminHubApi.Dtos.Auth;

public class AuthResponseDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Username { get; set; }
    public IList<string> Roles { get; set; }
}