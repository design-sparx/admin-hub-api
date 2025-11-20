using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/live-auctions")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.LiveAuctions)]
    public class AntdLiveAuctionsController : ControllerBase
    {
        private readonly IAntdLiveAuctionService _auctionService;

        public AntdLiveAuctionsController(IAntdLiveAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdLiveAuctionQueryParams queryParams)
        {
            var result = await _auctionService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _auctionService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdLiveAuctionDto dto)
        {
            var result = await _auctionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.AuctionId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdLiveAuctionDto dto)
        {
            var result = await _auctionService.UpdateAsync(id, dto);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _auctionService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
