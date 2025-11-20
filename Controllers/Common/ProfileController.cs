using System.Security.Claims;
using AdminHubApi.Dtos.Profile;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Common;

[ApiController]
[Route("/api/v1/profile")]
[Authorize] // All profile endpoints require authentication
[Tags("User Profile")]
public class ProfileController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ITokenBlacklistRepository _tokenBlacklistRepository;

    public ProfileController(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        ITokenBlacklistRepository tokenBlacklistRepository)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _tokenBlacklistRepository = tokenBlacklistRepository;
    }

    
    /// <summary>
    /// Get the current user's profile
    /// </summary>
    [HttpGet]
    [Authorize] // Any authenticated user can access their profile
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "User not authenticated" });
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = "User not found" });
        
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
    
        // Map to a user profile response
        var profile = new {
            id = user.Id,
            userName = user.UserName,
            email = user.Email,
            emailConfirmed = user.EmailConfirmed,
            phoneNumber = user.PhoneNumber,
            phoneNumberConfirmed = user.PhoneNumberConfirmed,
            twoFactorEnabled = user.TwoFactorEnabled,
            roles = roles,
            claims = claims.Select(c => new { type = c.Type, value = c.Value })
        };
    
        return Ok(profile);
    }

    /// <summary>
    /// Update the current user's profile
    /// </summary>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "User not authenticated" });

        // Get current user
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound(new { message = "User not found" });

        // Track if email is being changed
        bool isEmailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;

        // Update basic properties - only update properties that were provided
        if (isEmailChanged)
        {
            // Check if email is already in use by another account
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null && existingUser.Id != userId)
            {
                return BadRequest(new { message = "Email is already in use by another account" });
            }

            user.Email = model.Email;
            user.EmailConfirmed = false; // Require re-confirmation

            // In a production app, you'd send a confirmation email here
        }

        if (!string.IsNullOrEmpty(model.PhoneNumber) && user.PhoneNumber != model.PhoneNumber)
        {
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = false; // Require re-confirmation
        }

        // Add any other user-editable properties here
        // Note: Do NOT allow users to update their roles or other security settings

        // Save changes
        var result = await _userManager.UpdateAsync(user);

        if (!result.Success)
        {
            return BadRequest(new
            {
                message = "Failed to update profile",
                errors = result.Errors.Select(e => e.Description)
            });
        }

        // Return updated user profile
        return Ok(new
        {
            message = "Profile updated successfully",
            user = new
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                emailConfirmed = user.EmailConfirmed,
                phoneNumber = user.PhoneNumber,
                phoneNumberConfirmed = user.PhoneNumberConfirmed
            }
        });
    }
    
    /// <summary>
    /// Change the current user's password
    /// </summary>
    [HttpPost("/api/v1/profile/change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "User not authenticated" });
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = "User not found" });
    
        // Verify current password and change to new password
        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    
        if (!result.Success)
        {
            return BadRequest(new {
                message = "Failed to change password",
                errors = result.Errors.Select(e => e.Description)
            });
        }
    
        // Invalidate existing tokens after password change
        // Get the current token from the Authorization header
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            // Extract token
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenId = _tokenService.ExtractTokenId(token);
        
            if (!string.IsNullOrEmpty(tokenId))
            {
                // Get token expiration time
                var expiryTime = _tokenService.GetTokenExpirationTime(token);
            
                // Add token to blacklist using the repository
                await _tokenBlacklistRepository.BlacklistTokenAsync(tokenId, expiryTime);
            }
        }
    
        // In a real app, you might want to send an email notification about the password change
    
        return Ok(new { message = "Password changed successfully" });
    }
}