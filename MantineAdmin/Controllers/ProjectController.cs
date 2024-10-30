using MantineAdmin.Data;
using MantineAdmin.Dtos.Project;
using MantineAdmin.Mappers;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAllProjects()
    {
        var projects = _context.Projects.ToList().Select(s => s.ToProjectDto());

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public IActionResult GetProjectById([FromRoute] int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.ToProjectDto());
    }

    [HttpPost]
    public IActionResult CreateProject([FromBody] CreateProjectRequestDto projectDto)
    {
        var projectModel = projectDto.ToProjectFromCreateDto();
        _context.Projects.Add(projectModel);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProjectById), new { id = projectModel.Id }, projectModel.ToProjectDto());
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateProject([FromRoute] int id, [FromBody] UpdateProjectRequestDto projectDto)
    {
        var projectModel = _context.Projects.FirstOrDefault(p => p.Id == id);

        if (projectModel == null)
        {
            return NotFound();
        }

        projectModel.Name = projectDto.Name;
        projectModel.Description = projectDto.Description;
        projectModel.CreatedAt = projectDto.CreatedAt;

        _context.SaveChanges();

        return Ok(projectModel.ToProjectDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteProject([FromRoute] int id)
    {
        var projectModel = _context.Projects.FirstOrDefault(p => p.Id == id);

        if (projectModel == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(projectModel);

        _context.SaveChanges();

        return NoContent();
    }
}