using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/courses")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.Courses)]
    public class AntdCoursesController : ControllerBase
    {
        private readonly IAntdCourseService _courseService;

        public AntdCoursesController(IAntdCourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdCourseQueryParams queryParams)
        {
            var result = await _courseService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _courseService.GetByIdAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdCourseDto dto)
        {
            var result = await _courseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdCourseDto dto)
        {
            var result = await _courseService.UpdateAsync(id, dto);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _courseService.DeleteAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }
    }
}
