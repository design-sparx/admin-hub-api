using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize(Policy = "CanManageUsers")] // Apply policy-based authorization
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserService _userService;
    
    public RolesController(
        RoleManager<IdentityRole> roleManager,
        IUserService userService)
    {
        _roleManager = roleManager;
        _userService = userService;
    }
    
    /// <summary>
    /// Get all roles
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _userService.GetAllRolesAsync();
        return Ok(response);
    }
    
    /// <summary>
    /// Create a new role
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleDto model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
            return BadRequest(new { message = "Role name is required" });
            
        if (await _roleManager.RoleExistsAsync(model.Name))
            return BadRequest(new { message = "Role already exists" });
            
        var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
        
        if (!result.Succeeded)
            return BadRequest(new { 
                message = "Failed to create role",
                errors = result.Errors.Select(e => e.Description) 
            });
            
        return Ok(new { 
            succeeded = true,
            message = "Role created successfully" 
        });
    }
    
    /// <summary>
    /// Update a role
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleDto model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
            return BadRequest(new { message = "Role name is required" });
            
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
            return NotFound(new { message = "Role not found" });
            
        // Check if new name already exists on a different role
        if (role.Name != model.Name && await _roleManager.RoleExistsAsync(model.Name))
            return BadRequest(new { message = "Role name already exists" });
            
        role.Name = model.Name;
        var result = await _roleManager.UpdateAsync(role);
        
        if (!result.Succeeded)
            return BadRequest(new { 
                message = "Failed to update role",
                errors = result.Errors.Select(e => e.Description) 
            });
            
        return Ok(new { 
            succeeded = true,
            message = "Role updated successfully" 
        });
    }
    
    /// <summary>
    /// Delete a role
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
            return NotFound(new { message = "Role not found" });
        
        // In a real application, you'd check if the role is in use before deletion
        
        var result = await _roleManager.DeleteAsync(role);
        
        if (!result.Succeeded)
            return BadRequest(new { 
                message = "Failed to delete role",
                errors = result.Errors.Select(e => e.Description) 
            });
            
        return Ok(new { 
            succeeded = true,
            message = "Role deleted successfully" 
        });
    }
}