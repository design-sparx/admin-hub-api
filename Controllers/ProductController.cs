using AdminHubApi.Dtos;
using AdminHubApi.Entities;
using AdminHubApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProductController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectService.GetAllProductsAsync();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var project = await _projectService.GetProductByIdAsync(id);

            return Ok(project);
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectResponseDto projectDto)
    {
        await _projectService.AddProductAsync(projectDto);
        
        return CreatedAtAction(nameof(GetById), new { id = projectDto.Id }, projectDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProjectResponseDto projectDto)
    {
        try
        {
            await _projectService.UpdateProductAsync(id, projectDto);

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
            await _projectService.DeleteProductAsync(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}