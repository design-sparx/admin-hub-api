using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Data.Seeders
{
    public static class NormalUserSeeder
    {
        public static async Task SeedNormalUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            // Get admin user details from configuration
            var demoEmail = configuration["DemoUser:Email"] ?? "demo@adminhub.com";
            var demoUserName = configuration["DemoUser:UserName"] ?? "demo_user"; 
            var demoPassword = configuration["DemoUser:Password"] ?? "Demo@Pass1";

            // Check if admin user exists
            var demoUser = await userManager.FindByEmailAsync(demoEmail);

            if (demoUser == null)
            {
                logger.LogInformation($"Creating default demo user with email: {demoEmail}");
                
                demoUser = new ApplicationUser
                {
                    UserName = demoUserName,
                    Email = demoEmail,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(demoUser, demoPassword);

                if (result.Succeeded)
                {
                    logger.LogInformation($"Demo user created successfully");

                    // Add to the user role
                    await userManager.AddToRoleAsync(demoUser, RoleSeeder.UserRole);

                    // Add user permissions (new RBAC only)
                    var userPermissions = new List<Claim>
                    {
                        // Team permissions (collaborative access)
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.Projects),
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.Orders),
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.KanbanTasks),
                        new Claim(CustomClaimTypes.Permission, Permissions.Team.Analytics),

                        // User directory
                        new Claim(CustomClaimTypes.Permission, Permissions.Users.ViewDirectory),

                        // Personal permissions
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Profile),
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Invoices),
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Files),
                        new Claim(CustomClaimTypes.Permission, Permissions.Personal.Chats),
                    };

                    await userManager.AddClaimsAsync(demoUser, userPermissions);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors);
                    logger.LogError($"Failed to create demo user. Errors: {errors}");
                }
            }
            else
            {
                logger.LogInformation("Demo user already exists");
                
                // Ensure demo user is in user role
                if (!await userManager.IsInRoleAsync(demoUser, RoleSeeder.UserRole))
                {
                    await userManager.AddToRoleAsync(demoUser, RoleSeeder.UserRole);
                    logger.LogInformation("Added existing admin user to User role");
                }
            }
        }
    }
}