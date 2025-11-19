using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("api/v1/antd/delivery-analytics")]
    [ApiController]
    [PermissionAuthorize(Permissions.Antd.DeliveryAnalytics)]
    public class AntdDeliveryAnalyticsController : ControllerBase
    {
        private readonly IAntdDeliveryAnalyticService _deliveryAnalyticService;

        public AntdDeliveryAnalyticsController(IAntdDeliveryAnalyticService deliveryAnalyticService)
        {
            _deliveryAnalyticService = deliveryAnalyticService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AntdDeliveryAnalyticQueryParams queryParams)
        {
            var result = await _deliveryAnalyticService.GetAllAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _deliveryAnalyticService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Delivery analytic not found" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AntdDeliveryAnalyticCreateDto createDto)
        {
            var result = await _deliveryAnalyticService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AntdDeliveryAnalyticUpdateDto updateDto)
        {
            var result = await _deliveryAnalyticService.UpdateAsync(id, updateDto);
            if (result == null)
                return NotFound(new { message = "Delivery analytic not found" });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deliveryAnalyticService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = "Delivery analytic not found" });

            return NoContent();
        }
    }
}
