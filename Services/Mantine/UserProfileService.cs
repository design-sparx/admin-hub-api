using AdminHubApi.Data;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Entities.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdminHubApi.Services.Mantine
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserProfileService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileService(ApplicationDbContext context, ILogger<UserProfileService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrEmpty(userId))
                    return null;

                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.Id.ToString() == userId);

                if (userProfile == null)
                    return null;

                return new UserProfileDto
                {
                    Avatar = userProfile.Avatar ?? string.Empty,
                    Name = userProfile.Name,
                    Email = userProfile.Email,
                    Job = userProfile.Job ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user profile for user {UserId}", GetCurrentUserId());
                throw;
            }
        }

        public async Task<UserProfileDto?> UpdateUserProfileAsync(UserProfileDto userProfileDto)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (string.IsNullOrEmpty(userId))
                    return null;

                if (!Guid.TryParse(userId, out var guidUserId))
                    return null;

                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.Id == guidUserId);

                if (userProfile == null)
                {
                    // Create new profile if it doesn't exist
                    userProfile = new UserProfiles
                    {
                        Id = guidUserId,
                        Avatar = userProfileDto.Avatar,
                        Name = userProfileDto.Name,
                        Email = userProfileDto.Email,
                        Job = userProfileDto.Job,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.UserProfiles.Add(userProfile);
                }
                else
                {
                    // Update existing profile
                    userProfile.Avatar = userProfileDto.Avatar;
                    userProfile.Name = userProfileDto.Name;
                    userProfile.Email = userProfileDto.Email;
                    userProfile.Job = userProfileDto.Job;
                    userProfile.UpdatedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();

                return userProfileDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user profile for user {UserId}", GetCurrentUserId());
                throw;
            }
        }

        private string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}