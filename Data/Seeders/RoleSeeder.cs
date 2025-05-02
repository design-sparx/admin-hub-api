using System.Security.Claims;
using AdminHubApi.Constants;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class RoleSeeder
    {
        public readonly static string AdminRole = "Admin";
        public readonly static string UserRole = "User";
        public readonly static string ManagerRole = "Manager";

        // Define all application roles
        public readonly static string[] ApplicationRoles = new[]
        {
            AdminRole,
            UserRole,
            ManagerRole
        };

        // Define role permissions
        private readonly static Dictionary<string, string[]> RolePermissions = new()
        {
            {
                AdminRole, new[]
                {
                    Permissions.Users.View,
                    Permissions.Users.Create,
                    Permissions.Users.Edit,
                    Permissions.Users.Delete,
                    Permissions.Roles.View,
                    Permissions.Roles.Create,
                    Permissions.Roles.Edit,
                    Permissions.Roles.Delete
                    // Add other admin permissions
                }
            },
            {
                ManagerRole, new[]
                {
                    Permissions.Users.View,
                    Permissions.Users.Create,
                    Permissions.Users.Edit,
                    Permissions.Roles.View
                    // Add other manager permissions
                }
            },
            {
                UserRole, new[]
                {
                    Permissions.Users.View,
                    Permissions.Users.Edit,
                }
            }
        };

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityRole>>();

            logger.LogInformation("Starting role seeding process...");

            foreach (var roleName in ApplicationRoles)
            {
                await EnsureRoleExistsAsync(roleManager, roleName, logger);
            }

            // Seed role permissions
            foreach (var rolePermission in RolePermissions)
            {
                await AddPermissionsToRoleAsync(roleManager, rolePermission.Key, rolePermission.Value, logger);
            }

            logger.LogInformation("Role seeding completed successfully");
        }

        private static async Task EnsureRoleExistsAsync(
            RoleManager<IdentityRole> roleManager, 
            string roleName, 
            ILogger logger)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                logger.LogInformation($"Creating role: {roleName}");
                var role = new IdentityRole(roleName);
                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    logger.LogInformation($"Created role {roleName} successfully");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    logger.LogError($"Failed to create role {roleName}. Errors: {errors}");
                }
            }
            else
            {
                logger.LogInformation($"Role {roleName} already exists");
            }
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
                    if (!currentClaims.Any(c => c.Type == CustomClaimTypes.Permission && c.Value == permission))
                    {
                        logger.LogInformation($"Adding permission '{permission}' to role '{roleName}'");
                        await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                    }
                }
            }
        }
    }
}