using Microsoft.AspNetCore.Authorization;

namespace AdminHubApi.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string permission)
        {
            // Create a policy name with the permission prefix
            Policy = $"{PermissionAuthorizationPolicyProvider.PermissionPolicyPrefix}{permission}";
        }
    }
}