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

                    // Add to a User role (our simplified RBAC only has Admin/User)
                    await userManager.AddToRoleAsync(managerUser, RoleSeeder.UserRole);

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