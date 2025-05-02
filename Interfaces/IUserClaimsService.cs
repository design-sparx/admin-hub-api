using System.Security.Claims;
using AdminHubApi.Constants;

namespace AdminHubApi.Interfaces;

public interface IUserClaimsService
{
    Task<IList<Claim>> GetUserClaimsAsync(string userId);
    Task AddPermissionClaimsAsync(string userId, IEnumerable<string> permissions);
    Task RemovePermissionClaimsAsync(string userId, IEnumerable<string> permissions);
    Task AddClaimAsync(string userId, string claimType, string claimValue);
    Task RemoveClaimAsync(string userId, string claimType, string claimValue);
}