using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/scheduled-posts")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.ScheduledPosts)]
    public class AntdScheduledPostsController : ControllerBase
    {
        private readonly IAntdScheduledPostService _postService;

        public AntdScheduledPostsController(IAntdScheduledPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdScheduledPostQueryParams queryParams)
        {
            var result = await _postService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _postService.GetByIdAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdScheduledPostDto dto)
        {
            var result = await _postService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdScheduledPostDto dto)
        {
            var result = await _postService.UpdateAsync(id, dto);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _postService.DeleteAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }
    }
}
