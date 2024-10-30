using MantineAdmin.Data;
using MantineAdmin.Dtos.Project;
using MantineAdmin.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MantineAdmin.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public ProjectController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _context.Projects.ToListAsync();

        var projectsDto = projects.Select(s => s.ToProjectDto());

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById([FromRoute] int id)
    {
        var project = await _context.Projects.FindAsync(id);

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
        
        await _context.Projects.AddAsync(projectModel);
        
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjectById), new { id = projectModel.Id }, projectModel.ToProjectDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectRequestDto projectDto)
    {
        var projectModel = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if (projectModel == null)
        {
            return NotFound();
        }

        projectModel.Name = projectDto.Name;
        projectModel.Description = projectDto.Description;
        projectModel.CreatedAt = projectDto.CreatedAt;

        await _context.SaveChangesAsync();

        return Ok(projectModel.ToProjectDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        var projectModel = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        if (projectModel == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(projectModel);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
}