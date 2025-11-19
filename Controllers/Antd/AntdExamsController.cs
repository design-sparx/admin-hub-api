using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/exams")]
    [Authorize]
    public class AntdExamsController : AntdBaseController
    {
        private readonly IAntdExamService _examService;

        public AntdExamsController(IAntdExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        [PermissionAuthorize(Permissions.Antd.Exams)]
        public async Task<IActionResult> GetAll([FromQuery] AntdExamQueryParams queryParams)
        {
            var response = await _examService.GetAllAsync(queryParams);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(Permissions.Antd.Exams)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _examService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [PermissionAuthorize(Permissions.Antd.Exams)]
        public async Task<IActionResult> Create([FromBody] AntdExamDto dto)
        {
            var response = await _examService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Permissions.Antd.Exams)]
        public async Task<IActionResult> Update(string id, [FromBody] AntdExamDto dto)
        {
            try
            {
                var response = await _examService.UpdateAsync(id, dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Permissions.Antd.Exams)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _examService.DeleteAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
