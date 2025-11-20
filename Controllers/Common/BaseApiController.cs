using AdminHubApi.Dtos.ApiResponse;
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

        protected IActionResult SuccessResponse<T>(T data, string message = "Success", PaginationMeta? meta = null)
        {
            var response = new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Meta = meta,
                Timestamp = DateTime.UtcNow
            };
            return Ok(response);
        }

        protected IActionResult ErrorResponse(string message, int statusCode = 400, List<string>? errors = null)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>(),
                Timestamp = DateTime.UtcNow
            };
            return StatusCode(statusCode, response);
        }
    }
}