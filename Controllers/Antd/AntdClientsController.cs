using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/clients")]
    [Tags("Antd - Clients")]
    [PermissionAuthorize(Permissions.Antd.Clients)]
    public class AntdClientsController : AntdBaseController
    {
        private readonly IAntdClientService _clientService;

        public AntdClientsController(IAntdClientService clientService, ILogger<AntdClientsController> logger)
            : base(logger)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Get all clients with pagination and filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(AntdClientListResponse), 200)]
        public async Task<IActionResult> GetAllClients([FromQuery] AntdClientQueryParams queryParams)
        {
            try
            {
                var response = await _clientService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd clients");
                return ErrorResponse("Failed to retrieve clients", 500);
            }
        }

        /// <summary>
        /// Get client by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdClientResponse), 200)]
        public async Task<IActionResult> GetClientById(string id)
        {
            try
            {
                var response = await _clientService.GetByIdAsync(id);

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd client {ClientId}", id);
                return ErrorResponse("Failed to retrieve client", 500);
            }
        }

        /// <summary>
        /// Create new client
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AntdClientCreateResponse), 201)]
        public async Task<IActionResult> CreateClient([FromBody] AntdClientDto clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _clientService.CreateAsync(clientDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd client");
                return ErrorResponse("Failed to create client", 500);
            }
        }

        /// <summary>
        /// Update existing client
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdClientUpdateResponse), 200)]
        public async Task<IActionResult> UpdateClient(string id, [FromBody] AntdClientDto clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _clientService.UpdateAsync(id, clientDto);

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd client {ClientId}", id);
                return ErrorResponse("Failed to update client", 500);
            }
        }

        /// <summary>
        /// Delete client
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdClientDeleteResponse), 200)]
        public async Task<IActionResult> DeleteClient(string id)
        {
            try
            {
                var response = await _clientService.DeleteAsync(id);

                if (!response.Succeeded)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd client {ClientId}", id);
                return ErrorResponse("Failed to delete client", 500);
            }
        }
    }
}
