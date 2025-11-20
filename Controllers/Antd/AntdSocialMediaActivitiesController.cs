using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/social-media-activities")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.SocialMediaActivities)]
    public class AntdSocialMediaActivitiesController : ControllerBase
    {
        private readonly IAntdSocialMediaActivityService _activityService;

        public AntdSocialMediaActivitiesController(IAntdSocialMediaActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdSocialMediaActivityQueryParams queryParams)
        {
            var result = await _activityService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _activityService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdSocialMediaActivityDto dto)
        {
            var result = await _activityService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdSocialMediaActivityDto dto)
        {
            var result = await _activityService.UpdateAsync(id, dto);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _activityService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
