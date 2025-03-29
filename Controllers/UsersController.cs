using System.Security.Claims;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Policy = "CanManageUsers")] // Apply policy-based authorization
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET api/users
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string search = null)
    {
        var response = await _userService.GetAllUsersAsync(page, pageSize, search);

        return Ok(response);
    }

    // GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var response = await _userService.GetUserByIdAsync(id);

        if (!response.Succeeded)
            return NotFound(response);

        return Ok(response);
    }

    // POST api/users
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

    // PUT api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
    {
        // Skip ModelState validation for update operations
        // This allows partial updates

        var response = await _userService.UpdateUserAsync(id, model);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    // DELETE api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var response = await _userService.DeleteUserAsync(id);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    // POST api/users/{id}/change-password
    [HttpPost("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _userService.ChangePasswordAsync(id, model);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    // POST api/users/reset-password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _userService.ResetPasswordAsync(model);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    // PUT api/users/{id}/roles
    [HttpPut("{id}/roles")]
    public async Task<IActionResult> UpdateUserRoles(string id, [FromBody] List<string> roles)
    {
        var response = await _userService.UpdateUserRolesAsync(id, roles);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }
}