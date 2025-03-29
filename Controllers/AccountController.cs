using System.Security.Claims;
using AdminHubApi.Dtos.Auth;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/account")]
[Authorize] // Everything here requires authentication
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsService _userClaimsService;
    
    public AccountController(
        IUserService userService,
        UserManager<ApplicationUser> userManager,
        IUserClaimsService userClaimsService)
    {
        _userService = userService;
        _userManager = userManager;
        _userClaimsService = userClaimsService;
    }
    
    /// <summary>
    /// Get the current user's profile
    /// </summary>
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var response = await _userService.GetUserByIdAsync(userId);
        if (!response.Succeeded)
            return NotFound(response);
            
        return Ok(response);
    }
    
    /// <summary>
    /// Update the current user's profile
    /// </summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        // Remove role and claims from the model to prevent users from elevating their privileges
        model.Roles = null;
        model.Claims = null;
        
        var response = await _userService.UpdateUserAsync(userId, model);
        if (!response.Succeeded)
            return BadRequest(response);
            
        return Ok(response);
    }
    
    /// <summary>
    /// Change the current user's password
    /// </summary>
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
            return BadRequest(new { 
                message = "Password change failed",
                errors = result.Errors.Select(e => e.Description)
            });

        return Ok(new { message = "Password changed successfully" });
    }
    
    /// <summary>
    /// Get the current user's claims
    /// </summary>
    [HttpGet("claims")]
    public async Task<IActionResult> GetMyClaims()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var claims = await _userClaimsService.GetUserClaimsAsync(userId);
        
        return Ok(new
        {
            Claims = claims.Select(c => new { Type = c.Type, Value = c.Value })
        });
    }
}