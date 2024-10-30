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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comments = await _projectCommentRepository.GetAllAsync();

        var commentDto = comments.Select(s => s.ToProjectCommentDto());

        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectCommentById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comment = await _projectCommentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment.ToProjectCommentDto());
    }

    [HttpPost("{projectId:int}")]
    public async Task<IActionResult> CreateProjectComment([FromRoute] int projectId, CreateProjectCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _projectRepository.ProjectExists(projectId))
        {
            return BadRequest("Project does not exist!");
        }

        var commentModel = commentDto.ToCommentFromCreate(projectId);

        await _projectCommentRepository.CreateAsync(commentModel);

        return CreatedAtAction(nameof(GetProjectCommentById), new { id = commentModel.Id },
            commentModel.ToProjectCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteProjectComment([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var commentModel = await _projectCommentRepository.DeleteAsync(id);

        if (commentModel == null)
        {
            return NotFound("Project comment not found!");
        }

        return Ok(commentModel);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var commentModel = await _projectCommentRepository.UpdateAsync(id, commentDto);

        if (commentModel == null)
        {
            return NotFound();
        }

        return Ok(commentModel.ToProjectCommentDto());
    }
}