using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/pricings")]
    [Tags("Antd - Pricings")]
    [PermissionAuthorize(Permissions.Antd.Pricings)]
    public class AntdPricingsController : AntdBaseController
    {
        private readonly IAntdPricingService _pricingService;

        public AntdPricingsController(IAntdPricingService pricingService, ILogger<AntdPricingsController> logger)
            : base(logger)
        {
            _pricingService = pricingService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdPricingListResponse), 200)]
        public async Task<IActionResult> GetAllPricings([FromQuery] AntdPricingQueryParams queryParams)
        {
            try
            {
                var response = await _pricingService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd pricing plans");
                return ErrorResponse("Failed to retrieve pricing plans", 500);
            }
        }

        [HttpGet("plan/{plan}")]
        [ProducesResponseType(typeof(AntdPricingResponse), 200)]
        public async Task<IActionResult> GetPricingByPlan(string plan)
        {
            try
            {
                var response = await _pricingService.GetByPlanAsync(plan);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd pricing plan {Plan}", plan);
                return ErrorResponse("Failed to retrieve pricing plan", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdPricingResponse), 200)]
        public async Task<IActionResult> GetPricingById(string id)
        {
            try
            {
                var response = await _pricingService.GetByIdAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd pricing plan {PricingId}", id);
                return ErrorResponse("Failed to retrieve pricing plan", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdPricingCreateResponse), 201)]
        public async Task<IActionResult> CreatePricing([FromBody] AntdPricingDto pricingDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _pricingService.CreateAsync(pricingDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd pricing plan");
                return ErrorResponse("Failed to create pricing plan", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdPricingUpdateResponse), 200)]
        public async Task<IActionResult> UpdatePricing(string id, [FromBody] AntdPricingDto pricingDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _pricingService.UpdateAsync(id, pricingDto);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd pricing plan {PricingId}", id);
                return ErrorResponse("Failed to update pricing plan", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdPricingDeleteResponse), 200)]
        public async Task<IActionResult> DeletePricing(string id)
        {
            try
            {
                var response = await _pricingService.DeleteAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd pricing plan {PricingId}", id);
                return ErrorResponse("Failed to delete pricing plan", 500);
            }
        }
    }
}
