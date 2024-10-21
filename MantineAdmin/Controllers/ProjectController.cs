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
}