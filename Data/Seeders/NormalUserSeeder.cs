﻿using System.Security.Claims;
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

                    // Add all user permissions
                    var userPermissions = new List<Claim>
                    {
                        new Claim(CustomClaimTypes.Permission, Permissions.Users.View),
                        new Claim(CustomClaimTypes.Permission, Permissions.Users.Edit),
                        
                        new Claim(CustomClaimTypes.Permission, Permissions.Roles.View),
                        
                        new Claim(CustomClaimTypes.Permission, Permissions.Projects.View),
                        
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