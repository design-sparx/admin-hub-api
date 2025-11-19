using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/auction-creators")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.AuctionCreators)]
    public class AntdAuctionCreatorsController : ControllerBase
    {
        private readonly IAntdAuctionCreatorService _creatorService;

        public AntdAuctionCreatorsController(IAntdAuctionCreatorService creatorService)
        {
            _creatorService = creatorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdAuctionCreatorQueryParams queryParams)
        {
            var result = await _creatorService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _creatorService.GetByIdAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdAuctionCreatorDto dto)
        {
            var result = await _creatorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.CreatorId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdAuctionCreatorDto dto)
        {
            var result = await _creatorService.UpdateAsync(id, dto);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _creatorService.DeleteAsync(id);
            if (!result.Succeeded)
                return NotFound(result);
            return Ok(result);
        }
    }
}
