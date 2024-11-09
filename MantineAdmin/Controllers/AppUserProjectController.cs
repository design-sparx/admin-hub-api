using MantineAdmin.Extensions;
using MantineAdmin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MantineAdmin.Controllers;

[Route("api/user-project")]
[ApiController]
public class AppUserProjectController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IProjectRepository _projectRepository;
    private readonly IAppUserProjectRepository _appUserProjectRepository;

    public AppUserProjectController(UserManager<AppUser> userManager, IProjectRepository projectRepository, IAppUserProjectRepository appUserProjectRepository)
    {
        _userManager = userManager;
        _projectRepository = projectRepository;
        _appUserProjectRepository = appUserProjectRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserProjects()
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var userProject = await _appUserProjectRepository.GetUserProjects(appUser);
        
        return Ok(userProject);
    }
}