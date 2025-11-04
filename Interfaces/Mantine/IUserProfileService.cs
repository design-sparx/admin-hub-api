using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetUserProfileAsync();
        Task<UserProfileDto?> UpdateUserProfileAsync(UserProfileDto userProfileDto);
    }
}