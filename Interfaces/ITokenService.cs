using System.Security.Claims;
using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user, IList<string> roles);
    ClaimsPrincipal ValidateToken(string token);
}