using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class AdminUserSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            // Get admin user details from configuration
            var adminEmail = configuration["AdminUser:Email"] ?? "admin@adminhub.com";
            var adminUserName = configuration["AdminUser:UserName"] ?? "admin";
            var adminPassword = configuration["AdminUser:Password"] ?? "Admin@Password123!"; // Should be in secrets in production

            // Check if an admin user exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                logger.LogInformation($"Creating default admin user with email: {adminEmail}");
                
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    logger.LogInformation($"Admin user created successfully");
                    
                    // Add to the Admin role
                    await userManager.AddToRoleAsync(adminUser, RoleSeeder.AdminRole);
                    
                    // Add all admin permissions
                    var adminPermissions = new List<Claim>
                    {
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
                    
                    await userManager.AddClaimsAsync(adminUser, adminPermissions);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors);
                    logger.LogError($"Failed to create admin user. Errors: {errors}");
                }
            }
            else
            {
                logger.LogInformation("Admin user already exists");
                
                // Ensure admin user is in Admin role
                if (!await userManager.IsInRoleAsync(adminUser, RoleSeeder.AdminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, RoleSeeder.AdminRole);
                    logger.LogInformation("Added existing admin user to Admin role");
                }
            }
        }
    }
}