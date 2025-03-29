using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Services;

public class IdentitySeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public IdentitySeeder(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    
    public async Task SeedAsync()
    {
        // Create roles if they don't exist
        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            
        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));
            
        // Create admin user if it doesn't exist
        var adminUser = await _userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            
            var result = await _userManager.CreateAsync(adminUser, "Admin123!"); // Change this in production!
            if (result.Succeeded)
            {
                // Add admin to Admin role
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                
                // Add all permissions to admin
                var permissions = Permissions.GetAllPermissions();
                var permissionClaims = permissions.Select(p => new Claim(CustomClaimTypes.Permission, p));
                
                await _userManager.AddClaimsAsync(adminUser, permissionClaims);
                
                // Add subscription level
                await _userManager.AddClaimAsync(adminUser, new Claim(CustomClaimTypes.SubscriptionLevel, "Enterprise"));
                
                // Add department
                await _userManager.AddClaimAsync(adminUser, new Claim(CustomClaimTypes.Department, "Management"));
            }
        }
    }
}