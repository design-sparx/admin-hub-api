using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class UserPermissionUpdateSeeder
    {
        public static async Task UpdateUserPermissionsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationUser>>();

            logger.LogInformation("Starting user permissions update process...");

            // Update admin permissions
            var adminUser = await userManager.FindByEmailAsync("admin@adminhub.com");
            if (adminUser != null)
            {
                var adminPermissions = new List<Claim>
                {
                    // List all the expected permissions for admin users
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Roles.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Roles.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Roles.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Roles.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Delete),
                };
                
                await UpdateUserClaimsAsync(userManager, adminUser, adminPermissions, logger);
            }

            // Update manager permissions
            var managerUser = await userManager.FindByEmailAsync("manager@adminhub.com");
            if (managerUser != null)
            {
                var managerPermissions = new List<Claim>
                {
                    // List all the expected permissions for manager users
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Roles.View),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Edit),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Delete),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Edit),
                };
                
                await UpdateUserClaimsAsync(userManager, managerUser, managerPermissions, logger);
            }

            // Update normal user permissions
            var normalUser = await userManager.FindByEmailAsync("demo@adminhub.com");
            if (normalUser != null)
            {
                var normalUserPermissions = new List<Claim>
                {
                    // List all the expected permissions for normal users
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Roles.View),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Projects.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Products.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.ProductCategories.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Orders.Edit),
                    
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.View),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Create),
                    new Claim(CustomClaimTypes.Permission, Permissions.Invoices.Edit),
                };
                
                await UpdateUserClaimsAsync(userManager, normalUser, normalUserPermissions, logger);
            }

            logger.LogInformation("User permissions update completed successfully");
        }

        private static async Task UpdateUserClaimsAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            List<Claim> expectedClaims,
            ILogger logger)
        {
            var currentClaims = await userManager.GetClaimsAsync(user);
            
            // Find claims to add (those that exist in expected but not in current)
            foreach (var claim in expectedClaims)
            {
                if (!currentClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    logger.LogInformation($"Adding permission '{claim.Value}' to user '{user.UserName}'");
                    await userManager.AddClaimAsync(user, claim);
                }
            }
        }
    }
}