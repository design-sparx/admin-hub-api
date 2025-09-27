using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Common;

[ApiController]
[Route("/api/v1/countries")]
[Tags("Countries")]
public class CountriesController : ControllerBase
{
    private readonly ISystemConfigService _systemConfigService;
    private readonly ILogger<CountriesController> _logger;

    public CountriesController(ISystemConfigService systemConfigService, ILogger<CountriesController> logger)
    {
        _systemConfigService = systemConfigService;
        _logger = logger;
    }

    /// <summary>
    /// Get all countries (public endpoint - no authentication required)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCountries([FromQuery] bool? activeOnly = null)
    {
        try
        {
            var countries = await _systemConfigService.GetCountriesAsync();

            // Filter by active status if requested
            if (activeOnly.HasValue)
            {
                countries.Data = countries.Data?.Where(c => c.IsActive == activeOnly.Value).ToList();
            }

            return Ok(new
            {
                success = true,
                data = countries.Data,
                message = "Countries retrieved successfully",
                timestamp = DateTime.UtcNow,
                meta = new
                {
                    total = countries.Data?.Count ?? 0,
                    activeOnly = activeOnly
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving countries");

            return StatusCode(500, new
            {
                success = false,
                message = "Failed to retrieve countries",
                timestamp = DateTime.UtcNow
            });
        }
    }

    /// <summary>
    /// Get country by code
    /// </summary>
    [HttpGet("{code}")]
    public async Task<IActionResult> GetCountryByCode(string code)
    {
        try
        {
            var countries = await _systemConfigService.GetCountriesAsync();

            var country = countries.Data?.FirstOrDefault(c =>
                c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (country == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Country with code '{code}' not found",
                    timestamp = DateTime.UtcNow
                });
            }

            return Ok(new
            {
                success = true,
                data = country,
                message = "Country retrieved successfully",
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving country {CountryCode}", code);

            return StatusCode(500, new
            {
                success = false,
                message = "Failed to retrieve country",
                timestamp = DateTime.UtcNow
            });
        }
    }
}