using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/truck-delivery-requests")]
    [Tags("Antd - Truck Delivery Requests")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.TruckDeliveryRequests)]
    public class AntdTruckDeliveryRequestsController : ControllerBase
    {
        private readonly IAntdTruckDeliveryRequestService _truckDeliveryRequestService;

        public AntdTruckDeliveryRequestsController(IAntdTruckDeliveryRequestService truckDeliveryRequestService)
        {
            _truckDeliveryRequestService = truckDeliveryRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdTruckDeliveryRequestQueryParams queryParams)
        {
            var result = await _truckDeliveryRequestService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _truckDeliveryRequestService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Truck delivery request not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdTruckDeliveryRequestCreateDto createDto)
        {
            var result = await _truckDeliveryRequestService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AntdTruckDeliveryRequestUpdateDto updateDto)
        {
            var result = await _truckDeliveryRequestService.UpdateAsync(id, updateDto);
            if (result == null)
                return NotFound(new { message = "Truck delivery request not found" });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _truckDeliveryRequestService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = "Truck delivery request not found" });

            return NoContent();
        }
    }
}
