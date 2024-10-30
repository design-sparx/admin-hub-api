using MantineAdmin.Data;
using MantineAdmin.Dtos.Project;
using MantineAdmin.Interfaces;
using MantineAdmin.Mappers;
using MantineAdmin.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MantineAdmin.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IProjectRepository _projectRepository;

    public ProjectController(ApplicationDBContext context, IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectRepository.GetAllAsync();

        var projectsDto = projects.Select(s => s.ToProjectDto());

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById([FromRoute] int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.ToProjectDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequestDto projectDto)
    {
        var projectModel = projectDto.ToProjectFromCreateDto();

        await _projectRepository.CreateAsync(projectModel);

        return CreatedAtAction(nameof(GetProjectById), new { id = projectModel.Id }, projectModel.ToProjectDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectRequestDto projectDto)
    {
        var projectModel = await _projectRepository.UpdateAsync(id, projectDto);

        if (projectModel == null)
        {
            return NotFound();
        }

        return Ok(projectModel.ToProjectDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        var projectModel = await _projectRepository.DeleteAsync(id);

        if (projectModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}