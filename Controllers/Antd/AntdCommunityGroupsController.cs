using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/community-groups")]
    [Authorize]
    public class AntdCommunityGroupsController : AntdBaseController
    {
        private readonly IAntdCommunityGroupService _communityGroupService;

        public AntdCommunityGroupsController(IAntdCommunityGroupService communityGroupService)
        {
            _communityGroupService = communityGroupService;
        }

        [HttpGet]
        [PermissionAuthorize(Permissions.Antd.CommunityGroups)]
        public async Task<IActionResult> GetAll([FromQuery] AntdCommunityGroupQueryParams queryParams)
        {
            var response = await _communityGroupService.GetAllAsync(queryParams);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(Permissions.Antd.CommunityGroups)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _communityGroupService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [PermissionAuthorize(Permissions.Antd.CommunityGroups)]
        public async Task<IActionResult> Create([FromBody] AntdCommunityGroupDto dto)
        {
            var response = await _communityGroupService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Permissions.Antd.CommunityGroups)]
        public async Task<IActionResult> Update(string id, [FromBody] AntdCommunityGroupDto dto)
        {
            try
            {
                var response = await _communityGroupService.UpdateAsync(id, dto);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Permissions.Antd.CommunityGroups)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _communityGroupService.DeleteAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
