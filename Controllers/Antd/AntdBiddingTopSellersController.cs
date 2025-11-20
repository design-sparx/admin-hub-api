using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/bidding-top-sellers")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.BiddingTopSellers)]
    public class AntdBiddingTopSellersController : ControllerBase
    {
        private readonly IAntdBiddingTopSellerService _sellerService;

        public AntdBiddingTopSellersController(IAntdBiddingTopSellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdBiddingTopSellerQueryParams queryParams)
        {
            var result = await _sellerService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _sellerService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdBiddingTopSellerDto dto)
        {
            var result = await _sellerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AntdBiddingTopSellerDto dto)
        {
            var result = await _sellerService.UpdateAsync(id, dto);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _sellerService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
