using MantineAdmin.Data;
using MantineAdmin.Dtos.ProjectComment;
using MantineAdmin.Interfaces;
using MantineAdmin.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MantineAdmin.Controllers;

[Route("api/project/comment")]
[ApiController]
public class ProjectCommentController : ControllerBase
{
    private readonly IProjectCommentRepository _projectCommentRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectCommentController(IProjectCommentRepository projectCommentRepository,
        IProjectRepository projectRepository)
    {
        _projectCommentRepository = projectCommentRepository;
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectComments()
    {
        var comments = await _projectCommentRepository.GetAllAsync();

        var commentDto = comments.Select(s => s.ToProjectCommentDto());

        return Ok(commentDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectCommentById(int id)
    {
        var comment = await _projectCommentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment.ToProjectCommentDto());
    }

    [HttpPost("{projectId}")]
    public async Task<IActionResult> CreateProjectComment([FromRoute] int projectId, CreateProjectCommentDto commentDto)
    {
        if (!await _projectRepository.ProjectExists(projectId))
        {
            return BadRequest("Project does not exist!");
        }

        var commentModel = commentDto.ToCommentFromCreate(projectId);

        await _projectCommentRepository.CreateAsync(commentModel);

        return CreatedAtAction(nameof(GetProjectCommentById), new { id = commentModel.Id },
            commentModel.ToProjectCommentDto());
    }
}