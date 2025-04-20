using System.Security.Claims;
using AdminHubApi.Dtos.Auth;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signinManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenBlacklistRepository _tokenBlacklistRepository;

    public AuthController(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService, 
        IOptions<JwtSettings> jwtSettings,
        ITokenBlacklistRepository tokenBlacklistRepository)
    {
        _userManager = userManager;
        _signinManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
        _tokenBlacklistRepository = tokenBlacklistRepository;
    }

    /// <summary>
    /// Login with username and password
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
            return Unauthorized(new { message = "Username or password is incorrect" });

        var result = await _signinManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
            return Unauthorized(new { message = "Username or password is incorrect" });

        var userRoles = await _userManager.GetRolesAsync(user);
        var token = await _tokenService.GenerateJwtTokenAsync(user, userRoles);

        return Ok(new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Username = user.UserName,
            Roles = userRoles
        });
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _userManager.FindByNameAsync(request.Username) != null)
            return BadRequest(new { message = "Username already exists" });

        if (await _userManager.FindByEmailAsync(request.Email) != null)
            return BadRequest(new { message = "Email already exists" });

        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(new { 
                message = "User creation failed",
                errors = result.Errors.Select(e => e.Description)
            });

        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));

        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new { message = "User registered successfully" });
    }

    /// <summary>
    /// Request a password reset link
    /// </summary>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(request.Email);
        
        // Don't reveal that the user does not exist
        if (user == null)
            return Ok(new { message = "If your email is registered, you will receive a password reset link" });

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // In a real app, you would send an email with the token
        // For testing purposes, we'll return the token
        return Ok(new { 
            message = "If your email is registered, you will receive a password reset link",
            userId = user.Id,  // Include for testing only
            token = token      // Include for testing only
        });
    }

    /// <summary>
    /// Reset password using token
    /// </summary>
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

    /// <summary>
    /// Refresh an authentication token
    /// </summary>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Token))
            return BadRequest(new { message = "Token is required" });

        // Extract token ID (jti claim)
        var tokenId = _tokenService.ExtractTokenId(request.Token);
    
        if (string.IsNullOrEmpty(tokenId))
            return BadRequest(new { message = "Invalid token" });
        
        // Check if token is blacklisted
        if (await _tokenBlacklistRepository.IsTokenBlacklistedAsync(tokenId))
            return Unauthorized(new { message = "Token has been revoked" });

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

        // Blacklist the old token
        var expiryTime = _tokenService.GetTokenExpirationTime(request.Token);
        await _tokenBlacklistRepository.BlacklistTokenAsync(tokenId, expiryTime);

        return Ok(new AuthResponseDto
        {
            Token = newToken,
            Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Username = user.UserName,
            Roles = roles
        });
    }
    
    /// <summary>
    /// Logout with username and password
    /// </summary>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] AuthRequestDto request)
    {
        // Get the current token from the Authorization header
        var authorizationHeader = Request.Headers["Authorization"].ToString();
    
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return BadRequest(new { message = "Invalid token format" });
        }
    
        // Extract token
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
    
        // Get token ID (jti claim)
        var tokenId = _tokenService.ExtractTokenId(token);
    
        if (string.IsNullOrEmpty(tokenId))
        {
            return BadRequest(new { message = "Invalid token" });
        }
    
        // Get token expiration time
        var expiryTime = _tokenService.GetTokenExpirationTime(token);
    
        // Add token to blacklist using the repository
        await _tokenBlacklistRepository.BlacklistTokenAsync(tokenId, expiryTime);
    
        return Ok(new { message = "Logged out successfully" });
    }
}