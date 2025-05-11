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
                    RoleSeeder.AdminRole, [
                        Permissions.Users.View,
                        Permissions.Users.Create,
                        Permissions.Users.Edit,
                        Permissions.Users.Delete,

                        Permissions.Roles.View,
                        Permissions.Roles.Create,
                        Permissions.Roles.Edit,
                        Permissions.Roles.Delete,

                        Permissions.Projects.View,
                        Permissions.Projects.Create,
                        Permissions.Projects.Edit,
                        Permissions.Projects.Delete,

                        Permissions.Products.View,
                        Permissions.Products.Create,
                        Permissions.Products.Edit,
                        Permissions.Products.Delete,

                        Permissions.ProductCategories.View,
                        Permissions.ProductCategories.Create,
                        Permissions.ProductCategories.Edit,
                        Permissions.ProductCategories.Delete,

                        Permissions.Orders.View,
                        Permissions.Orders.Create,
                        Permissions.Orders.Edit,
                        Permissions.Orders.Delete,

                        Permissions.Invoices.View,
                        Permissions.Invoices.Create,
                        Permissions.Invoices.Edit,
                        Permissions.Invoices.Delete,
                    ]
                },
                {
                    RoleSeeder.ManagerRole, [
                        Permissions.Users.View,
                        Permissions.Users.Create,
                        Permissions.Users.Edit,

                        Permissions.Roles.View,

                        Permissions.Projects.View,
                        Permissions.Projects.Create,
                        Permissions.Projects.Edit,
                        Permissions.Projects.Delete,

                        Permissions.Products.View,
                        Permissions.Products.Create,
                        Permissions.Products.Edit,

                        Permissions.ProductCategories.View,
                        Permissions.ProductCategories.Create,
                        Permissions.ProductCategories.Edit,

                        Permissions.Orders.View,
                        Permissions.Orders.Create,
                        Permissions.Orders.Edit,

                        Permissions.Invoices.View,
                        Permissions.Invoices.Create,
                        Permissions.Invoices.Edit,
                    ]
                },
                {
                    RoleSeeder.UserRole, [
                        Permissions.Users.View,
                        Permissions.Users.Edit,

                        Permissions.Roles.View,

                        Permissions.Projects.View,

                        Permissions.Products.View,
                        Permissions.Products.Create,
                        Permissions.Products.Edit,

                        Permissions.ProductCategories.View,
                        Permissions.ProductCategories.Create,
                        Permissions.ProductCategories.Edit,

                        Permissions.Orders.View,
                        Permissions.Orders.Create,
                        Permissions.Orders.Edit,

                        Permissions.Invoices.View,
                        Permissions.Invoices.Create,
                        Permissions.Invoices.Edit,
                    ]
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