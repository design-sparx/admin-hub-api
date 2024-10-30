using MantineAdmin.Data;
using MantineAdmin.Interfaces;
using MantineAdmin.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MantineAdmin.Controllers;

[Route("api/project/comment")]
[ApiController]
public class ProjectCommentController : ControllerBase
{
    private readonly IProjectCommentRepository _projectCommentRepository;

    public ProjectCommentController(IProjectCommentRepository projectCommentRepository)
    {
        _projectCommentRepository = projectCommentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectComments()
    {
        var comments = await _projectCommentRepository.GetAllAsync();

        var commentDto = comments.Select(s => s.ToProjectCommentDto());
        
        return Ok(commentDto);
    }
}