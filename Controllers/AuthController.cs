using AdminHubApi.Dtos.Auth;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signinManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService, 
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signinManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
    {
        // Check if model is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if user exists
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
            return Unauthorized(new { message = "Username or password is incorrect" });

        // Verify password
        var result = await _signinManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded) // FIXED: Check for NOT succeeded
            return Unauthorized(new { message = "Username or password is incorrect" });

        // Get user roles
        var userRoles = await _userManager.GetRolesAsync(user);

        // Generate token
        var token = await _tokenService.GenerateJwtTokenAsync(user, userRoles);

        // Return token and user info
        return Ok(new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Username = user.UserName,
            Roles = userRoles
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        // Check if model is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if username already exists
        if (await _userManager.FindByNameAsync(request.Username) != null)
            return BadRequest(new { message = "Username already exists" });

        // Check if email already exists
        if (await _userManager.FindByEmailAsync(request.Email) != null)
            return BadRequest(new { message = "Email already exists" });

        // Create new user
        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        // Add user to database
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(new { 
                message = "User creation failed",
                errors = result.Errors.Select(e => e.Description)
            });

        // Add user to default role
        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));

        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new { message = "User registered successfully" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        return Ok(new
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            Roles = roles,
            Claims = claims.Select(c => new { Type = c.Type, Value = c.Value })
        });
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
            return BadRequest(new { 
                message = "Password change failed",
                errors = result.Errors.Select(e => e.Description)
            });

        return Ok(new { message = "Password changed successfully" });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ChangePasswordRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(request.Email);
        
        // Don't reveal that the user does not exist
        if (user == null)
            return Ok(new { message = "If your email is registered, you will receive a password reset link" });

        // Generate password reset token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // In a real app, you would send an email with the token
        // For testing purposes, we'll return the token
        return Ok(new { 
            message = "If your email is registered, you will receive a password reset link",
            userId = user.Id,  // Include for testing only
            token = token      // Include for testing only
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByIdAsync(request.UserId);
        
        if (user == null)
            return BadRequest(new { message = "Invalid user ID" });

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
            return BadRequest(new { 
                message = "Password reset failed",
                errors = result.Errors.Select(e => e.Description)
            });

        return Ok(new { message = "Password reset successful" });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Token))
            return BadRequest(new { message = "Token is required" });

        var principal = _tokenService.ValidateToken(request.Token);
        
        if (principal == null)
            return Unauthorized(new { message = "Invalid token" });

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Invalid token" });

        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return Unauthorized(new { message = "User not found" });

        var roles = await _userManager.GetRolesAsync(user);
        var newToken = await _tokenService.GenerateJwtTokenAsync(user, roles);

        return Ok(new AuthResponseDto
        {
            Token = newToken,
            Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Username = user.UserName,
            Roles = roles
        });
    }
}