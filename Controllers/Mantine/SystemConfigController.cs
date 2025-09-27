using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine")]
    [Tags("Mantine - System Configuration")]
    public class SystemConfigController : MantineBaseController
    {
        private readonly ISystemConfigService _systemConfigService;

        public SystemConfigController(ISystemConfigService systemConfigService, ILogger<SystemConfigController> logger)
            : base(logger)
        {
            _systemConfigService = systemConfigService;
        }

        /// <summary>
        /// Get all supported languages
        /// </summary>
        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguages()
        {
            try
            {
                var languages = await _systemConfigService.GetLanguagesAsync();
                return Ok(new
                {
                    success = true,
                    data = languages.Data,
                    message = "Languages retrieved successfully",
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving languages");
                return ErrorResponse("Failed to retrieve languages", 500);
            }
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _systemConfigService.GetCountriesAsync();
                return Ok(new
                {
                    success = true,
                    data = countries.Data,
                    message = "Countries retrieved successfully",
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving countries");
                return ErrorResponse("Failed to retrieve countries", 500);
            }
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