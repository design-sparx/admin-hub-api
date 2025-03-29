using System.Security.Claims;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Extenstions;

public static class IdentityExtensions
{
    // Add claims to user
    public static async Task<IdentityResult> AddUserClaimsAync(this UserManager<ApplicationUser> userManager,
        ApplicationUser user, IEnumerable<Claim> claims)
    {
        var userClaims = await userManager.GetClaimsAsync(user);

        foreach (var claim in claims)
        {
            // Only add if the claim doesn't exist in user
            if (!userClaims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                await userManager.AddClaimAsync(user, claim);
            }
        }
        
        return IdentityResult.Success;
    }
    
    // Replace all claims of a specific type
    public static async Task<IdentityResult> ReplaceUserClaimsAsync(this UserManager<ApplicationUser> userManager,
        ApplicationUser user, string claimType, IEnumerable<string> claimValues)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var claimsToRemove = userClaims.Where(c => c.Type == claimType).ToList();

        foreach (var claim in claimsToRemove)
        {
            await userManager.RemoveClaimAsync(user, claim);
        }

        foreach (var value in claimValues)
        {
            await userManager.AddClaimAsync(user, new Claim(claimType, value));
        }
        
        return IdentityResult.Success;
    }
}