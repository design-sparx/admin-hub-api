using System.Security.Claims;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

// Controllers/UserManagementController.cs
[Route("api/[controller]")]
[ApiController]
[Authorize] // Require authentication for all actions
public class UserManagementController : ControllerBase
{
    // View users - requires specific permission
    [HttpGet]
    [Authorize(Policy = "CanViewUsers")]
    public IActionResult GetUsers()
    {
        return Ok(new { message = "You can view users" });
    }
    
    // Create user - requires specific permission
    [HttpPost]
    [Authorize(Policy = "CanManageUsers")]
    public IActionResult CreateUser()
    {
        return Ok(new { message = "You can create users" });
    }
    
    // Premium feature - requires subscription level
    [HttpGet("premium-feature")]
    [Authorize(Policy = "PremiumFeatures")]
    public IActionResult PremiumFeature()
    {
        return Ok(new { message = "You have access to premium features" });
    }
    
    // Get current user's claims
    [HttpGet("my-claims")]
    public async Task<IActionResult> GetMyClaims([FromServices] IUserClaimsService userClaimsService)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var claims = await userClaimsService.GetUserClaimsAsync(userId);
        
        return Ok(new
        {
            Claims = claims.Select(c => new { Type = c.Type, Value = c.Value })
        });
    }
}