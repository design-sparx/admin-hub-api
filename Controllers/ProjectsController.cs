using AdminHubApi.Dtos.Projects;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProjectStatus? status = null)
    {
        if (status.HasValue)
        {
            var filteredProjects = await _projectService.GetProjectsByStatusAsync(status.Value);

            return Ok(filteredProjects);
        }
        else
        {
            var allProjects = await _projectService.GetAllProjectsAsync();

            return Ok(allProjects);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            return Ok(project);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDto projectDto)
    {
        var createdProjectIdResponse = await _projectService.AddProjectAsync(projectDto);
        
        if (!createdProjectIdResponse.Succeeded)
        {
            return BadRequest(createdProjectIdResponse);
        }
        
        var createdProjectId = createdProjectIdResponse.Data;
        var createdProjectResponse = await _projectService.GetProjectByIdAsync(createdProjectId);
        
        return createdProjectResponse.Succeeded 
            ? CreatedAtAction(nameof(GetById), new { id = createdProjectId }, createdProjectResponse) 
            : BadRequest(createdProjectResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProjectResponseDto projectDto)
    {
        try
        {
            await _projectService.UpdateProjectAsync(id, projectDto);

            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _projectService.DeleteProjectAsync(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}