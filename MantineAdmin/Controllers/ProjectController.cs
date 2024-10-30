using MantineAdmin.Data;
using MantineAdmin.Dtos.Project;
using MantineAdmin.Helpers;
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
    public async Task<IActionResult> GetAllProjects([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var projects = await _projectRepository.GetAllAsync(query);

        var projectsDto = projects.Select(s => s.ToProjectDto());

        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var projectModel = projectDto.ToProjectFromCreateDto();

        await _projectRepository.CreateAsync(projectModel);

        return CreatedAtAction(nameof(GetProjectById), new { id = projectModel.Id }, projectModel.ToProjectDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectRequestDto projectDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var projectModel = await _projectRepository.UpdateAsync(id, projectDto);

        if (projectModel == null)
        {
            return NotFound();
        }

        return Ok(projectModel.ToProjectDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var projectModel = await _projectRepository.DeleteAsync(id);

        if (projectModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}