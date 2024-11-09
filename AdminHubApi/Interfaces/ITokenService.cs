using AdminHubApi.Models;

namespace AdminHubApi.Interfaces;

public interface ITokenService
{
    string GenerateToken(AppUser user);
}