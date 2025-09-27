using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/user-profile")]
    [Tags("Mantine - User Profile")]
    public class UserProfileController : MantineBaseController
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService, ILogger<UserProfileController> logger)
            : base(logger)
        {
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// Get current user profile
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                var userProfile = await _userProfileService.GetUserProfileAsync();
                if (userProfile == null)
                    return NotFound(new { success = false, message = "User profile not found" });

                return SuccessResponse(userProfile, "User profile retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user profile");
                return ErrorResponse("Failed to retrieve user profile", 500);
            }
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDto userProfileDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userProfile = await _userProfileService.UpdateUserProfileAsync(userProfileDto);
                if (userProfile == null)
                    return NotFound(new { success = false, message = "User profile not found" });

                return SuccessResponse(userProfile, "User profile updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user profile");
                return ErrorResponse("Failed to update user profile", 500);
            }
        }
    }
}