using System.Security.Claims;
using AdminHubApi.Constants;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class PermissionUpdateSeeder
    {
        public static async Task UpdateRolePermissionsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityRole>>();

            logger.LogInformation("Starting role permissions update process...");

            // Project permissions to add
            var projectPermissions = new Dictionary<string, string[]>
            {
                {
                    RoleSeeder.AdminRole, new[]
                    {
                        Permissions.Projects.View,
                        Permissions.Projects.Create,
                        Permissions.Projects.Edit,
                        Permissions.Projects.Delete
                    }
                },
                {
                    RoleSeeder.ManagerRole, new[]
                    {
                        Permissions.Projects.View,
                        Permissions.Projects.Create,
                        Permissions.Projects.Edit,
                        Permissions.Projects.Delete
                    }
                },
                {
                    RoleSeeder.UserRole, new[]
                    {
                        Permissions.Projects.View
                    }
                }
            };

            // Update permissions for each role
            foreach (var rolePermission in projectPermissions)
            {
                await AddPermissionsToRoleAsync(roleManager, rolePermission.Key, rolePermission.Value, logger);
            }

            logger.LogInformation("Role permissions update completed successfully");
        }

        private static async Task AddPermissionsToRoleAsync(
            RoleManager<IdentityRole> roleManager,
            string roleName,
            string[] permissions,
            ILogger logger)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                var role = await roleManager.FindByNameAsync(roleName);
                var currentClaims = await roleManager.GetClaimsAsync(role);

                foreach (var permission in permissions)
                {
                    // Only add the permission if it doesn't already exist
                    if (!currentClaims.Any(c => c.Type == CustomClaimTypes.Permission && c.Value == permission))
                    {
                        logger.LogInformation($"Adding permission '{permission}' to role '{roleName}'");
                        await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                    }
                    else
                    {
                        logger.LogDebug($"Permission '{permission}' already exists for role '{roleName}'");
                    }
                }
            }
            else
            {
                logger.LogWarning($"Role '{roleName}' doesn't exist - cannot add permissions");
            }
        }
    }
}