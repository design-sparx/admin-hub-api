using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/test-jwt")]
[Authorize] // This requires authentication
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "You are authenticated!" });
    }
    
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")] // This requires Admin role
    public IActionResult GetAdmin()
    {
        return Ok(new { message = "You are an admin!" });
    }
}