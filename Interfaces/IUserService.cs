using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.UserManagement;

namespace AdminHubApi.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserDto>> GetUserByIdAsync(string userId);
    Task<ApiResponse<List<UserDto>>> GetAllUsersAsync(int page = 1, int pageSize = 10, string searchTerm = null);
    Task<ApiResponse<UserDto>> CreateUserAsync(CreateUserDto model);
    Task<ApiResponse<UserDto>> UpdateUserAsync(string userId, UpdateUserDto model);
    Task<ApiResponse<bool>> DeleteUserAsync(string userId);
    Task<ApiResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto model);
    Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordDto model);
    Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync();
    Task<ApiResponse<UserDto>> UpdateUserRolesAsync(string userId, List<string> roles);
}