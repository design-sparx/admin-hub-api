using AdminHubApi.Interfaces;
using AdminHubApi.Extensions;
using AdminHubApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[Route("api/user-project")]
[ApiController]
public class AppUserProjectController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IProjectRepository _projectRepository;
    private readonly IAppUserProjectRepository _appUserProjectRepository;

    public AppUserProjectController(UserManager<AppUser> userManager, IProjectRepository projectRepository,
        IAppUserProjectRepository appUserProjectRepository)
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddUserProject(int projectId)
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project == null)
            return BadRequest("Project not found");

        if (appUser == null)
            return BadRequest("User not found");

        var userProject = await _appUserProjectRepository.GetUserProjects(appUser);

        if (userProject.Any(x => x.Id == projectId))
            return BadRequest("Project already exists, cannot add project!");

        var appUserProjectModel = new AppUserProject
        {
            ProjectId = project.Id,
            AppUserId = appUser.Id,
        };

        await _appUserProjectRepository.CreateAsync(appUserProjectModel);

        return Created();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUserProject(int projectId)
    {
        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);

        if (appUser == null)
            return BadRequest("User not found");

        var userProject = await _appUserProjectRepository.GetUserProjects(appUser);

        var filteredProjects = userProject.Where(x => x.Id == projectId).ToList();

        if (filteredProjects.Count() == 1)
        {
            await _appUserProjectRepository.DeleteAsync(appUser, projectId);
        }
        else
        {
            return BadRequest("Project not found");
        }

        return Ok();
    }
}