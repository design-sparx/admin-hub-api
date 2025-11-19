using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/social-media-stats")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.SocialMediaStats)]
    public class AntdSocialMediaStatsController : ControllerBase
    {
        private readonly IAntdSocialMediaStatsService _statsService;

        public AntdSocialMediaStatsController(IAntdSocialMediaStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdSocialMediaStatsQueryParams queryParams)
        {
            var result = await _statsService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _statsService.GetByIdAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdSocialMediaStatsDto dto)
        {
            var result = await _statsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdSocialMediaStatsDto dto)
        {
            var result = await _statsService.UpdateAsync(id, dto);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _statsService.DeleteAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }
    }
}
