using AdminHubApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AdminHubApi.Security
{
    // This class dynamically creates authorization policies for permissions
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public const string PERMISSION_POLICY_PREFIX = "Permission:";
        
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) 
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // First check if this is a permission policy
            if (policyName.StartsWith(PERMISSION_POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                // Extract the permission from the policy name
                var permission = policyName.Substring(PERMISSION_POLICY_PREFIX.Length);
                
                // Create a policy requiring the permission claim
                var policy = new AuthorizationPolicyBuilder()
                    .RequireClaim(CustomClaimTypes.Permission, permission)
                    .Build();
                    
                return policy;
            }
            
            // If not a permission policy, use the base implementation
            return await base.GetPolicyAsync(policyName);
        }
    }
}