using System.Security.Claims;
using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user, IList<string> roles);
    Task<ClaimsPrincipal> ValidateTokenAsync(string token);
    ClaimsPrincipal ValidateToken(string token);
    DateTime GetTokenExpirationTime(string token);
    string ExtractTokenId(string token);
}