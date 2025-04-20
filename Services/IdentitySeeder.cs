using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Services;

public class IdentitySeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public IdentitySeeder(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task SeedAsync()
    {
        // Create roles if they don't exist
        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            
        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));
            
        // Create admin user if it doesn't exist
        var adminUser = await _userManager.FindByEmailAsync("admin@example.com");
        var demoUser = await _userManager.FindByEmailAsync("demo@example.com");
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                Id = "6362b986-14c9-4901-8cf0-b2c2bb067907",
                UserName = "super_admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            
            // Get password from configuration
            var adminPassword = _configuration["AdminUser:Password"] ?? "Admin@Pass1";
            
            var result = await _userManager.CreateAsync(adminUser, adminPassword);
            
            if (result.Succeeded)
            {
                // Add admin to Admin role
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                
                // Add all permissions to admin
                var permissions = Permissions.GetAllPermissions();
                var permissionClaims = permissions.Select(p => new Claim(CustomClaimTypes.Permission, p));
                
                await _userManager.AddClaimsAsync(adminUser, permissionClaims);
                
                // // Add subscription level
                // await _userManager.AddClaimAsync(adminUser, new Claim(CustomClaimTypes.SubscriptionLevel, "Enterprise"));
                //
                // // Add department
                // await _userManager.AddClaimAsync(adminUser, new Claim(CustomClaimTypes.Department, "Management"));
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create admin user: {errors}");
                Console.WriteLine($"Using admin password (length: {adminPassword.Length}): {adminPassword}");
            }
        }
        
        if (demoUser == null)
        {
            demoUser = new ApplicationUser
            {
                Id = "2261f335-da25-49a2-af14-faf23e34b704",
                UserName = "demo_user",
                Email = "demo@example.com",
                EmailConfirmed = true
            };
            
            // Get password from configuration
            var demoPassword = _configuration["DemoUser:Password"] ?? "Demo@Pass1";
            
            var result = await _userManager.CreateAsync(demoUser, demoPassword);
            
            if (result.Succeeded)
            {
                // Add admin to User role
                await _userManager.AddToRoleAsync(demoUser, "User");
                
                // Add all permissions to admin
                var permissions = Permissions.GetAllPermissions();
                var permissionClaims = permissions.Select(p => new Claim(CustomClaimTypes.Permission, p));
                
                await _userManager.AddClaimsAsync(demoUser, permissionClaims);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create demo user: {errors}");
                Console.WriteLine($"Using admin password (length: {demoPassword.Length}): {demoPassword}");
            }
        }
    }
}