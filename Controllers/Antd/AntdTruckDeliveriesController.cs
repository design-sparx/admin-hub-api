using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/truck-deliveries")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.TruckDeliveries)]
    public class AntdTruckDeliveriesController : ControllerBase
    {
        private readonly IAntdTruckDeliveryService _truckDeliveryService;

        public AntdTruckDeliveriesController(IAntdTruckDeliveryService truckDeliveryService)
        {
            _truckDeliveryService = truckDeliveryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdTruckDeliveryQueryParams queryParams)
        {
            var result = await _truckDeliveryService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _truckDeliveryService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Truck delivery not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdTruckDeliveryCreateDto createDto)
        {
            var result = await _truckDeliveryService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AntdTruckDeliveryUpdateDto updateDto)
        {
            var result = await _truckDeliveryService.UpdateAsync(id, updateDto);
            if (result == null)
                return NotFound(new { message = "Truck delivery not found" });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _truckDeliveryService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = "Truck delivery not found" });

            return NoContent();
        }
    }
}
