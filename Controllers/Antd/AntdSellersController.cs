using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/sellers")]
    [Tags("Antd - Sellers")]
    [PermissionAuthorize(Permissions.Antd.Sellers)]
    public class AntdSellersController : AntdBaseController
    {
        private readonly IAntdSellerService _sellerService;

        public AntdSellersController(IAntdSellerService sellerService, ILogger<AntdSellersController> logger)
            : base(logger)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdSellerListResponse), 200)]
        public async Task<IActionResult> GetAllSellers([FromQuery] AntdSellerQueryParams queryParams)
        {
            try
            {
                var response = await _sellerService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd sellers");
                return ErrorResponse("Failed to retrieve sellers", 500);
            }
        }

        [HttpGet("top")]
        [ProducesResponseType(typeof(AntdSellerListResponse), 200)]
        public async Task<IActionResult> GetTopSellers([FromQuery] int limit = 10)
        {
            try
            {
                var response = await _sellerService.GetTopSellersAsync(limit);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top Antd sellers");
                return ErrorResponse("Failed to retrieve top sellers", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdSellerResponse), 200)]
        public async Task<IActionResult> GetSellerById(string id)
        {
            try
            {
                var response = await _sellerService.GetByIdAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd seller {SellerId}", id);
                return ErrorResponse("Failed to retrieve seller", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdSellerCreateResponse), 201)]
        public async Task<IActionResult> CreateSeller([FromBody] AntdSellerDto sellerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _sellerService.CreateAsync(sellerDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd seller");
                return ErrorResponse("Failed to create seller", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdSellerUpdateResponse), 200)]
        public async Task<IActionResult> UpdateSeller(string id, [FromBody] AntdSellerDto sellerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _sellerService.UpdateAsync(id, sellerDto);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd seller {SellerId}", id);
                return ErrorResponse("Failed to update seller", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdSellerDeleteResponse), 200)]
        public async Task<IActionResult> DeleteSeller(string id)
        {
            try
            {
                var response = await _sellerService.DeleteAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd seller {SellerId}", id);
                return ErrorResponse("Failed to delete seller", 500);
            }
        }
    }
}
