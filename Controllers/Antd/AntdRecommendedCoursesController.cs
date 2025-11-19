using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/recommended-courses")]
    [Authorize]
    public class AntdRecommendedCoursesController : AntdBaseController
    {
        private readonly IAntdRecommendedCourseService _recommendedCourseService;

        public AntdRecommendedCoursesController(IAntdRecommendedCourseService recommendedCourseService)
        {
            _recommendedCourseService = recommendedCourseService;
        }

        [HttpGet]
        [PermissionAuthorize(Permissions.Antd.RecommendedCourses)]
        public async Task<IActionResult> GetAll([FromQuery] AntdRecommendedCourseQueryParams queryParams)
        {
            var response = await _recommendedCourseService.GetAllAsync(queryParams);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(Permissions.Antd.RecommendedCourses)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _recommendedCourseService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [PermissionAuthorize(Permissions.Antd.RecommendedCourses)]
        public async Task<IActionResult> Create([FromBody] AntdRecommendedCourseDto dto)
        {
            var response = await _recommendedCourseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Permissions.Antd.RecommendedCourses)]
        public async Task<IActionResult> Update(string id, [FromBody] AntdRecommendedCourseDto dto)
        {
            try
            {
                var response = await _recommendedCourseService.UpdateAsync(id, dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Permissions.Antd.RecommendedCourses)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _recommendedCourseService.DeleteAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
