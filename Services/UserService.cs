using System.Security.Claims;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ApiResponse<UserDto>> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };

        return new ApiResponse<UserDto>
        {
            Success = true,
            Data = await MapToUserDtoAsync(user)
        };
    }

    public async Task<ApiResponse<List<UserDto>>> GetAllUsersAsync(int page = 1, int pageSize = 10,
        string searchTerm = null)
    {
        IQueryable<ApplicationUser> usersQuery = _userManager.Users;

        // Apply search
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();

            usersQuery = usersQuery.Where(u =>
                u.UserName.ToLower().Contains(searchTerm) ||
                u.Email.ToLower().Contains(searchTerm));
        }

        // Calculate total count for pagination
        var totalCount = await usersQuery.CountAsync();

        // Apply pagination
        var users = await usersQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Map to DTOs
        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            userDtos.Add(await MapToUserDtoAsync(user));
        }

        return new ApiResponse<List<UserDto>>
        {
            Success = true,
            Data = userDtos,
            // You could add pagination info to the response if needed
            // PaginationInfo = new { Page = page, PageSize = pageSize, TotalCount = totalCount, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) }
        };
    }

    public async Task<ApiResponse<UserDto>> CreateUserAsync(CreateUserDto model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "Failed to create user",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };

        // Assign roles
        if (model.Roles != null && model.Roles.Any())
        {
            var roles = model.Roles.Where(r => _roleManager.RoleExistsAsync(r).Result).ToList();

            if (roles.Any())
            {
                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        // Assign claims
        if (model.Claims != null && model.Claims.Any())
        {
            var claims = model.Claims.Select(c => new Claim(c.Type, c.Value)).ToList();
            await _userManager.AddClaimsAsync(user, claims);
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User created successfully",
            Data = await MapToUserDtoAsync(user)
        };
    }

    public async Task<ApiResponse<UserDto>> UpdateUserAsync(string userId, UpdateUserDto model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };

        // Update basic properties - only update properties that were provided
        if (!string.IsNullOrWhiteSpace(model.Email) && user.Email != model.Email)
        {
            user.Email = model.Email;
            user.EmailConfirmed = false; // Require re-confirmation
        }

        if (!string.IsNullOrWhiteSpace(model.PhoneNumber) && user.PhoneNumber != model.PhoneNumber)
        {
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = false; // Require re-confirmation
        }

        if (model.LockoutEnabled.HasValue)
        {
            user.LockoutEnabled = model.LockoutEnabled.Value;
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "Failed to update user",
                Errors = result.Errors.Select(e => e.Description).ToList()
            };

        // Update roles only if provided
        if (model.Roles != null)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            var rolesToRemove = currentRoles.Except(model.Roles).ToList();

            var rolesToAdd = model.Roles.Except(currentRoles)
                .Where(r => _roleManager.RoleExistsAsync(r).Result)
                .ToList();

            if (rolesToRemove.Any())
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (rolesToAdd.Any())
                await _userManager.AddToRolesAsync(user, rolesToAdd);
        }

        // Update claims only if provided
        if (model.Claims != null)
        {
            var currentClaims = await _userManager.GetClaimsAsync(user);

            // Remove all existing claims
            foreach (var claim in currentClaims)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // Add new claims
            var claims = model.Claims.Select(c => new Claim(c.Type, c.Value)).ToList();
            await _userManager.AddClaimsAsync(user, claims);
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User updated successfully",
            Data = await MapToUserDtoAsync(user)
        };
    }

    public async Task<ApiResponse<bool>> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "User not found",
                Data = false
            };

        // You might want to check if user has important data/relationships before deletion

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Failed to delete user",
                Errors = result.Errors.Select(e => e.Description).ToList(),
                Data = false
            };

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "User deleted successfully",
            Data = true
        };
    }

    public async Task<ApiResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "User not found",
                Data = false
            };

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Failed to change password",
                Errors = result.Errors.Select(e => e.Description).ToList(),
                Data = false
            };

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Password changed successfully",
            Data = true
        };
    }

    public async Task<ApiResponse<bool>> ResetPasswordAsync(ResetPasswordDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "User not found",
                Data = false
            };

        // Generate a password reset token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Reset the password
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

        if (!result.Succeeded)
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Failed to reset password",
                Errors = result.Errors.Select(e => e.Description).ToList(),
                Data = false
            };

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Password reset successfully",
            Data = true
        };
    }

    public async Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        var roleDtos = roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name
        }).ToList();

        return new ApiResponse<List<RoleDto>>
        {
            Success = true,
            Data = roleDtos
        };
    }

    public async Task<ApiResponse<UserDto>> UpdateUserRolesAsync(string userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found"
            };

        var currentRoles = await _userManager.GetRolesAsync(user);

        // Remove all existing roles
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Add new roles
        if (roles != null && roles.Any())
        {
            var validRoles = roles.Where(r => _roleManager.RoleExistsAsync(r).Result).ToList();

            if (validRoles.Any())
                await _userManager.AddToRolesAsync(user, validRoles);
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User roles updated successfully",
            Data = await MapToUserDtoAsync(user)
        };
    }

    // Helper method to map ApplicationUser to UserDto
    private async Task<UserDto> MapToUserDtoAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            Roles = roles.ToList(),
            Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value }).ToList()
        };
    }
}