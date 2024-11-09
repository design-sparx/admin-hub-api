using AdminHubApi.Dtos.ProjectComment;
using AdminHubApi.Helpers;
using AdminHubApi.Interfaces;
using AdminHubApi.Data;
using AdminHubApi.Extensions;
using AdminHubApi.Mappers;
using AdminHubApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[Route("api/project/comment")]
[ApiController]
public class ProjectCommentController : ControllerBase
{
    private readonly IProjectCommentRepository _projectCommentRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly UserManager<AppUser> _userManager;

    public ProjectCommentController(IProjectCommentRepository projectCommentRepository,
        IProjectRepository projectRepository, UserManager<AppUser> userManager)
    {
        _projectCommentRepository = projectCommentRepository;
        _projectRepository = projectRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comments = await _projectCommentRepository.GetAllAsync(query);

        var commentDto = comments.Select(s => s.ToProjectCommentDto());

        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
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
    public async Task<IActionResult> Create([FromRoute] int projectId, CreateProjectCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _projectRepository.ProjectExists(projectId))
        {
            return BadRequest("Project does not exist!");
        }

        var username = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(username);

        if (appUser == null)
            return BadRequest("User not found!");

        var commentModel = commentDto.ToCommentFromCreate(projectId);
        commentModel.AppUserId = appUser.Id;
        
        await _projectCommentRepository.CreateAsync(commentModel);

        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id },
            commentModel.ToProjectCommentDto());
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjectCommentDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var commentModel = await _projectCommentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate(id));

        if (commentModel == null)
        {
            return NotFound("Project comment not found!");
        }

        return Ok(commentModel.ToProjectCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
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
}