using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AdminHubApi.Controllers.Common
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly ILogger _logger;

        protected BaseApiController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult SuccessResponse<T>(T data, string message = "Success")
        {
            return Ok(new
            {
                success = true,
                data = data,
                message = message,
                timestamp = DateTime.UtcNow
            });
        }

        protected IActionResult ErrorResponse(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new
            {
                success = false,
                message = message,
                timestamp = DateTime.UtcNow
            });
        }
    }
}