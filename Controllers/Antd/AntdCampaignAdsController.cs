using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/campaign-ads")]
    [Tags("Antd - Campaign Ads")]
    [PermissionAuthorize(Permissions.Antd.CampaignAds)]
    public class AntdCampaignAdsController : AntdBaseController
    {
        private readonly IAntdCampaignAdService _campaignAdService;

        public AntdCampaignAdsController(IAntdCampaignAdService campaignAdService, ILogger<AntdCampaignAdsController> logger)
            : base(logger)
        {
            _campaignAdService = campaignAdService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdCampaignAdListResponse), 200)]
        public async Task<IActionResult> GetAllCampaignAds([FromQuery] AntdCampaignAdQueryParams queryParams)
        {
            try
            {
                var response = await _campaignAdService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd campaign ads");
                return ErrorResponse("Failed to retrieve campaign ads", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdCampaignAdResponse), 200)]
        public async Task<IActionResult> GetCampaignAdById(string id)
        {
            try
            {
                var response = await _campaignAdService.GetByIdAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd campaign ad {AdId}", id);
                return ErrorResponse("Failed to retrieve campaign ad", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdCampaignAdCreateResponse), 201)]
        public async Task<IActionResult> CreateCampaignAd([FromBody] AntdCampaignAdDto campaignAdDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _campaignAdService.CreateAsync(campaignAdDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd campaign ad");
                return ErrorResponse("Failed to create campaign ad", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdCampaignAdUpdateResponse), 200)]
        public async Task<IActionResult> UpdateCampaignAd(string id, [FromBody] AntdCampaignAdDto campaignAdDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _campaignAdService.UpdateAsync(id, campaignAdDto);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd campaign ad {AdId}", id);
                return ErrorResponse("Failed to update campaign ad", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdCampaignAdDeleteResponse), 200)]
        public async Task<IActionResult> DeleteCampaignAd(string id)
        {
            try
            {
                var response = await _campaignAdService.DeleteAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd campaign ad {AdId}", id);
                return ErrorResponse("Failed to delete campaign ad", 500);
            }
        }
    }
}
