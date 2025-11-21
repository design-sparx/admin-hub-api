using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/trucks")]
    [Tags("Antd - Trucks")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.Trucks)]
    public class AntdTrucksController : ControllerBase
    {
        private readonly IAntdTruckService _truckService;

        public AntdTrucksController(IAntdTruckService truckService)
        {
            _truckService = truckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdTruckQueryParams queryParams)
        {
            var result = await _truckService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _truckService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Truck not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdTruckCreateDto createDto)
        {
            var result = await _truckService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AntdTruckUpdateDto updateDto)
        {
            var result = await _truckService.UpdateAsync(id, updateDto);
            if (result == null)
                return NotFound(new { message = "Truck not found" });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _truckService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = "Truck not found" });

            return NoContent();
        }
    }
}
