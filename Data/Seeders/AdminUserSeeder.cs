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
                    
                    // Add admin permissions (new RBAC only)
                    var adminPermissions = new List<Claim>
                    {
                        // Admin permissions
                        new Claim(CustomClaimTypes.Permission, Permissions.Admin.UserManagement),
                        new Claim(CustomClaimTypes.Permission, Permissions.Admin.SystemSettings),

                        // Team permissions
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.Projects),
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.Orders),
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.KanbanTasks),
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.Analytics),

                        // User directory
                        new Claim(CustomClaimTypes.Permission, Permissions.Users.ViewDirectory),

                        // Personal permissions (admin can access all)
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Profile),
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Invoices),
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Files),
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Chats),
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