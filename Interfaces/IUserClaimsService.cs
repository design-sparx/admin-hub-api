using System.Security.Claims;
using AdminHubApi.Constants;

namespace AdminHubApi.Interfaces;

public interface IUserClaimsService
{
    Task<IList<Claim>> GetUserClaimsAsync(string userId);
    Task AddPermissionClaimsAsync(string userId, IEnumerable<string> permissions);
    Task RemovePermissionClaimsAsync(string userId, IEnumerable<string> permissions);
    Task SetDepartmentAsync(string userId, string department);
    Task SetSubscriptionLevelAsync(string userId, string level);
}