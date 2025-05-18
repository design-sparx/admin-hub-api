using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdminHubApi.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenBlacklistRepository _tokenBlacklistRepository;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        UserManager<ApplicationUser> userManager,
        ITokenBlacklistRepository tokenBlacklistRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _tokenBlacklistRepository = tokenBlacklistRepository;
    }

    public async Task<string> GenerateJwtTokenAsync(ApplicationUser user, IList<string> roles)
    {
        // Get user claims from UserManager
        var userClaims = await _userManager.GetClaimsAsync(user);
        
        // Generate a unique token ID
        var tokenId = Guid.NewGuid().ToString();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            // Add a unique identifier for this token
            new Claim(JwtRegisteredClaimNames.Jti, tokenId),
        };

        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Add custom claims from user
        claims.AddRange(userClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
        
        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
    
    public async Task<ClaimsPrincipal> ValidateTokenAsync(string token)
    {
        // Extract token ID
        var tokenId = ExtractTokenId(token);
        
        // Check if token is blacklisted
        if (!string.IsNullOrEmpty(tokenId) && await _tokenBlacklistRepository.IsTokenBlacklistedAsync(tokenId))
        {
            return null;
        }
        
        // Perform normal token validation
        return ValidateToken(token);
    }
    
    public DateTime GetTokenExpirationTime(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        if (tokenHandler.CanReadToken(token))
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.ValidTo;
        }
        
        return DateTime.MinValue;
    }
    
    public string ExtractTokenId(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        if (tokenHandler.CanReadToken(token))
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
        }
        
        return null;
    }
    
    public ClaimsPrincipal ValidateTokenForRefresh(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, // This is the key change - don't validate lifetime for refresh
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    
        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
}