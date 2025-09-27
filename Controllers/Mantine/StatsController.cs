using AdminHubApi.Interfaces.Mantine;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/stats")]
    [Tags("Mantine - Analytics")]
    public class StatsController : MantineBaseController
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService, ILogger<StatsController> logger)
            : base(logger)
        {
            _statsService = statsService;
        }

        /// <summary>
        /// Get dashboard statistics grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = await _statsService.GetStatsAsync();
                return SuccessResponse(stats, "Dashboard statistics retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard statistics");
                return ErrorResponse("Failed to retrieve dashboard statistics", 500);
            }
        }

        /// <summary>
        /// Get all individual stats
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllStats()
        {
            try
            {
                var stats = await _statsService.GetAllStatsAsync();
                return SuccessResponse(stats, "All statistics retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all statistics");
                return ErrorResponse("Failed to retrieve statistics", 500);
            }
        }

        /// <summary>
        /// Get stat by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatById(int id)
        {
            try
            {
                var stat = await _statsService.GetStatByIdAsync(id);
                if (stat == null)
                    return NotFound(new { success = false, message = "Statistic not found" });

                return SuccessResponse(stat, "Statistic retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving statistic {StatId}", id);
                return ErrorResponse("Failed to retrieve statistic", 500);
            }
        }
    }
}