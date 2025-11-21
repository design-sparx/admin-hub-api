using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/employees")]
    [Tags("Antd - Employees")]
    [PermissionAuthorize(Permissions.Antd.Employees)]
    public class AntdEmployeesController : AntdBaseController
    {
        private readonly IAntdEmployeeService _employeeService;

        public AntdEmployeesController(IAntdEmployeeService employeeService, ILogger<AntdEmployeesController> logger)
            : base(logger)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdEmployeeListResponse), 200)]
        public async Task<IActionResult> GetAllEmployees([FromQuery] AntdEmployeeQueryParams queryParams)
        {
            try
            {
                var response = await _employeeService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd employees");
                return ErrorResponse("Failed to retrieve employees", 500);
            }
        }

        [HttpGet("statistics")]
        [ProducesResponseType(typeof(AntdEmployeeStatisticsResponse), 200)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var response = await _employeeService.GetStatisticsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd employee statistics");
                return ErrorResponse("Failed to retrieve statistics", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdEmployeeResponse), 200)]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            try
            {
                var response = await _employeeService.GetByIdAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd employee {EmployeeId}", id);
                return ErrorResponse("Failed to retrieve employee", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdEmployeeCreateResponse), 201)]
        public async Task<IActionResult> CreateEmployee([FromBody] AntdEmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _employeeService.CreateAsync(employeeDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd employee");
                return ErrorResponse("Failed to create employee", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdEmployeeUpdateResponse), 200)]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] AntdEmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _employeeService.UpdateAsync(id, employeeDto);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd employee {EmployeeId}", id);
                return ErrorResponse("Failed to update employee", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdEmployeeDeleteResponse), 200)]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                var response = await _employeeService.DeleteAsync(id);
                if (!response.Succeeded) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd employee {EmployeeId}", id);
                return ErrorResponse("Failed to delete employee", 500);
            }
        }
    }
}
