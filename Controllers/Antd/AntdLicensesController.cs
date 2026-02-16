using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/licenses")]
    [Tags("Antd - Licenses")]
    [PermissionAuthorize(Permissions.Antd.Licenses)]
    public class AntdLicensesController : AntdBaseController
    {
        private readonly IAntdLicenseService _licenseService;

        public AntdLicensesController(IAntdLicenseService licenseService, ILogger<AntdLicensesController> logger)
            : base(logger)
        {
            _licenseService = licenseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdLicenseListResponse), 200)]
        public async Task<IActionResult> GetAllLicenses([FromQuery] AntdLicenseQueryParams queryParams)
        {
            try
            {
                var response = await _licenseService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd licenses");
                return ErrorResponse("Failed to retrieve licenses", 500);
            }
        }

        [HttpGet("title/{title}")]
        [ProducesResponseType(typeof(AntdLicenseResponse), 200)]
        public async Task<IActionResult> GetLicenseByTitle(string title)
        {
            try
            {
                var response = await _licenseService.GetByTitleAsync(title);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd license {Title}", title);
                return ErrorResponse("Failed to retrieve license", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdLicenseResponse), 200)]
        public async Task<IActionResult> GetLicenseById(string id)
        {
            try
            {
                var response = await _licenseService.GetByIdAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd license {LicenseId}", id);
                return ErrorResponse("Failed to retrieve license", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdLicenseCreateResponse), 201)]
        public async Task<IActionResult> CreateLicense([FromBody] AntdLicenseDto licenseDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _licenseService.CreateAsync(licenseDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd license");
                return ErrorResponse("Failed to create license", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdLicenseUpdateResponse), 200)]
        public async Task<IActionResult> UpdateLicense(string id, [FromBody] AntdLicenseDto licenseDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _licenseService.UpdateAsync(id, licenseDto);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd license {LicenseId}", id);
                return ErrorResponse("Failed to update license", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdLicenseDeleteResponse), 200)]
        public async Task<IActionResult> DeleteLicense(string id)
        {
            try
            {
                var response = await _licenseService.DeleteAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd license {LicenseId}", id);
                return ErrorResponse("Failed to delete license", 500);
            }
        }
    }
}
