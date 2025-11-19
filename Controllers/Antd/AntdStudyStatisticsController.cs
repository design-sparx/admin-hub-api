using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/study-statistics")]
    [Authorize]
    public class AntdStudyStatisticsController : AntdBaseController
    {
        private readonly IAntdStudyStatisticService _studyStatisticService;

        public AntdStudyStatisticsController(IAntdStudyStatisticService studyStatisticService)
        {
            _studyStatisticService = studyStatisticService;
        }

        [HttpGet]
        [PermissionAuthorize(Permissions.Antd.StudyStatistics)]
        public async Task<IActionResult> GetAll([FromQuery] AntdStudyStatisticQueryParams queryParams)
        {
            var response = await _studyStatisticService.GetAllAsync(queryParams);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(Permissions.Antd.StudyStatistics)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _studyStatisticService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [PermissionAuthorize(Permissions.Antd.StudyStatistics)]
        public async Task<IActionResult> Create([FromBody] AntdStudyStatisticDto dto)
        {
            var response = await _studyStatisticService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Permissions.Antd.StudyStatistics)]
        public async Task<IActionResult> Update(string id, [FromBody] AntdStudyStatisticDto dto)
        {
            try
            {
                var response = await _studyStatisticService.UpdateAsync(id, dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Permissions.Antd.StudyStatistics)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _studyStatisticService.DeleteAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
