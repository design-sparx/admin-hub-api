using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Common;

[ApiController]
[Route("/api/v1/languages")]
[Tags("Languages")]
public class LanguagesController : ControllerBase
{
    private readonly ISystemConfigService _systemConfigService;
    private readonly ILogger<LanguagesController> _logger;

    public LanguagesController(ISystemConfigService systemConfigService, ILogger<LanguagesController> logger)
    {
        _systemConfigService = systemConfigService;
        _logger = logger;
    }

    /// <summary>
    /// Get all languages (public endpoint - no authentication required)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetLanguages([FromQuery] bool? activeOnly = null)
    {
        try
        {
            var languages = await _systemConfigService.GetLanguagesAsync();

            // Filter by active status if requested
            if (activeOnly.HasValue)
            {
                languages.Data = languages.Data?.Where(l => l.IsActive == activeOnly.Value).ToList();
            }

            return Ok(new
            {
                success = true,
                data = languages.Data,
                message = "Languages retrieved successfully",
                timestamp = DateTime.UtcNow,
                meta = new
                {
                    total = languages.Data?.Count ?? 0,
                    activeOnly = activeOnly
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving languages");

            return StatusCode(500, new
            {
                success = false,
                message = "Failed to retrieve languages",
                timestamp = DateTime.UtcNow
            });
        }
    }

    /// <summary>
    /// Get language by code
    /// </summary>
    [HttpGet("{code}")]
    public async Task<IActionResult> GetLanguageByCode(string code)
    {
        try
        {
            var languages = await _systemConfigService.GetLanguagesAsync();

            var language = languages.Data?.FirstOrDefault(l =>
                l.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (language == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Language with code '{code}' not found",
                    timestamp = DateTime.UtcNow
                });
            }

            return Ok(new
            {
                success = true,
                data = language,
                message = "Language retrieved successfully",
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving language {LanguageCode}", code);

            return StatusCode(500, new
            {
                success = false,
                message = "Failed to retrieve language",
                timestamp = DateTime.UtcNow
            });
        }
    }
}