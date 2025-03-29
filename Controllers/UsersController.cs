﻿using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Policy = "CanManageUsers")] // Apply policy-based authorization for all admin functions
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserClaimsService _userClaimsService;

    public UsersController(
        IUserService userService,
        IUserClaimsService userClaimsService)
    {
        _userService = userService;
        _userClaimsService = userClaimsService;
    }

    /// <summary>
    /// Get a list of all users (admin only)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string search = null)
    {
        var response = await _userService.GetAllUsersAsync(page, pageSize, search);

        return Ok(response);
    }

    /// <summary>
    /// Get a specific user by ID (admin only)
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var response = await _userService.GetUserByIdAsync(id);

        if (!response.Succeeded)
            return NotFound(response);

        return Ok(response);
    }

    /// <summary>
    /// Create a new user (admin only)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _userService.CreateUserAsync(model);

        if (!response.Succeeded)
            return BadRequest(response);

        return CreatedAtAction(nameof(GetUser), new { id = response.Data.Id }, response);
    }

    /// <summary>
    /// Update a user (admin only)
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
    {
        // Allow partial updates - skipping model validation
        var response = await _userService.UpdateUserAsync(id, model);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Delete a user (admin only)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var response = await _userService.DeleteUserAsync(id);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Admin reset password for a user - doesn't require old password
    /// </summary>
    [HttpPost("{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(string id, [FromBody] ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _userService.ResetPasswordAsync(model);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Update roles for a user (admin only)
    /// </summary>
    [HttpPut("{id}/roles")]
    public async Task<IActionResult> UpdateUserRoles(string id, [FromBody] List<string> roles)
    {
        var response = await _userService.UpdateUserRolesAsync(id, roles);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Update claims for a user (admin only)
    /// </summary>
    [HttpPut("{id}/claims")]
    public async Task<IActionResult> UpdateUserClaims(string id, [FromBody] List<ClaimDto> claims)
    {
        if (claims == null)
            return BadRequest(new { message = "Claims are required" });

        var user = await _userService.GetUserByIdAsync(id);

        if (!user.Succeeded)
            return NotFound(new { message = "User not found" });

        try
        {
            // Get existing claims
            var existingClaims = await _userClaimsService.GetUserClaimsAsync(id);

            // Group claims by specific types we handle differently
            var permissionClaims =
                claims.Where(c => c.Type == CustomClaimTypes.Permission).Select(c => c.Value).ToList();

            var departmentClaim = claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Department)?.Value;
            var subscriptionClaim = claims.FirstOrDefault(c => c.Type == CustomClaimTypes.SubscriptionLevel)?.Value;

            // Handle permissions using specialized method
            if (permissionClaims.Any())
            {
                // First remove all existing permissions
                var existingPermissions = existingClaims
                    .Where(c => c.Type == CustomClaimTypes.Permission)
                    .Select(c => c.Value)
                    .ToList();

                if (existingPermissions.Any())
                {
                    await _userClaimsService.RemovePermissionClaimsAsync(id, existingPermissions);
                }

                // Then add the new ones
                await _userClaimsService.AddPermissionClaimsAsync(id, permissionClaims);
            }

            // Handle department claim using specialized method
            if (!string.IsNullOrEmpty(departmentClaim))
            {
                await _userClaimsService.SetDepartmentAsync(id, departmentClaim);
            }

            // Handle subscription level claim using specialized method
            if (!string.IsNullOrEmpty(subscriptionClaim))
            {
                await _userClaimsService.SetSubscriptionLevelAsync(id, subscriptionClaim);
            }

            // Handle all other claim types using general methods
            var otherClaims = claims.Where(c =>
                c.Type != CustomClaimTypes.Permission &&
                c.Type != CustomClaimTypes.Department &&
                c.Type != CustomClaimTypes.SubscriptionLevel).ToList();

            // Remove existing claims of types that aren't in the specialized categories
            foreach (var claim in existingClaims)
            {
                if (claim.Type != CustomClaimTypes.Permission &&
                    claim.Type != CustomClaimTypes.Department &&
                    claim.Type != CustomClaimTypes.SubscriptionLevel)
                {
                    await _userClaimsService.RemoveClaimAsync(id, claim.Type, claim.Value);
                }
            }

            // Add all other claims
            foreach (var claim in otherClaims)
            {
                await _userClaimsService.AddClaimAsync(id, claim.Type, claim.Value);
            }

            return Ok(new { message = "User claims updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Failed to update claims", error = ex.Message });
        }
    }
}