using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "CanManageUsers")] // Apply policy-based authorization
public class RolesController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public RolesController(
        IUserService userService,
        RoleManager<IdentityRole> roleManager)
    {
        _userService = userService;
        _roleManager = roleManager;
    }
    
    // GET api/roles
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var response = await _userService.GetAllRolesAsync();
        return Ok(response);
    }
    
    // POST api/roles
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
    
    // DELETE api/roles/{name}
    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteRole(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);
        if (role == null)
            return NotFound(new { message = "Role not found" });
            
        // Check if role is in use
        // This would be better implemented with proper checks in a real application
        
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