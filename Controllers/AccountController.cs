using System.Security.Claims;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires authentication
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public AccountController(
        IUserService userService,
        UserManager<ApplicationUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }
    
    // GET api/account/profile
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
    
    // PUT api/account/profile
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
    
    // POST api/account/change-password
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
            
        var response = await _userService.ChangePasswordAsync(userId, model);
        if (!response.Succeeded)
            return BadRequest(response);
            
        return Ok(response);
    }
}