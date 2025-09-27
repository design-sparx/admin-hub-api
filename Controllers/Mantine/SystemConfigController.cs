using AdminHubApi.Constants;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine")]
    [Tags("Mantine - System Configuration")]
    [PermissionAuthorize(Permissions.Admin.SystemSettings)]
    public class SystemConfigController : MantineBaseController
    {
        private readonly ISystemConfigService _systemConfigService;

        public SystemConfigController(ISystemConfigService systemConfigService, ILogger<SystemConfigController> logger)
            : base(logger)
        {
            _systemConfigService = systemConfigService;
        }


        /// <summary>
        /// Get traffic analytics data
        /// </summary>
        [HttpGet("traffic")]
        public async Task<IActionResult> GetTraffic([FromQuery] TrafficQueryParams queryParams)
        {
            try
            {
                var traffic = await _systemConfigService.GetTrafficAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = traffic.Data,
                    message = "Traffic data retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = traffic.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving traffic data");
                return ErrorResponse("Failed to retrieve traffic data", 500);
            }
        }
    }
}