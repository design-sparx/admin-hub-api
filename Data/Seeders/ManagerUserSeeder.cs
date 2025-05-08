using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class ManagerUserSeeder
    {
        public static async Task SeedManagerUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            // Get manager user details from configuration
            var managerEmail = configuration["ManagerUser:Email"] ?? "manager@adminhub.com";
            var managerUserName = configuration["ManagerUser:UserName"] ?? "manager_user";
            var managerPassword = configuration["ManagerUser:Password"] ?? "Manager@Pass1";

            // Check if a manager user exists
            var managerUser = await userManager.FindByEmailAsync(managerEmail);

            if (managerUser == null)
            {
                logger.LogInformation($"Creating default manager user with email: {managerEmail}");

                managerUser = new ApplicationUser
                {
                    UserName = managerUserName,
                    Email = managerEmail,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(managerUser, managerPassword);

                if (result.Succeeded)
                {
                    logger.LogInformation($"Manager user created successfully");

                    // Add to a Manager role
                    await userManager.AddToRoleAsync(managerUser, RoleSeeder.ManagerRole);

                    // Add all user permissions
                    var userPermissions = new List<Claim>
                    {
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
                    };

                    await userManager.AddClaimsAsync(managerUser, userPermissions);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors);
                    logger.LogError($"Failed to create manager user. Errors: {errors}");
                }
            }
            else
            {
                logger.LogInformation("Manager user already exists");

                // Ensure a manager is in the manager role
                if (!await userManager.IsInRoleAsync(managerUser, RoleSeeder.ManagerRole))
                {
                    await userManager.AddToRoleAsync(managerUser, RoleSeeder.ManagerRole);
                    logger.LogInformation("Added existing manager user to manager role");
                }
            }
        }
    }
}