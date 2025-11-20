using AdminHubApi.Constants;
using AdminHubApi.Dtos.Antd;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Antd
{
    [Route("/api/v1/antd/orders")]
    [Tags("Antd - Orders")]
    [PermissionAuthorize(Permissions.Antd.Orders)]
    public class AntdOrdersController : AntdBaseController
    {
        private readonly IAntdOrderService _orderService;

        public AntdOrdersController(IAntdOrderService orderService, ILogger<AntdOrdersController> logger)
            : base(logger)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AntdOrderListResponse), 200)]
        public async Task<IActionResult> GetAllOrders([FromQuery] AntdOrderQueryParams queryParams)
        {
            try
            {
                var response = await _orderService.GetAllAsync(queryParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd orders");
                return ErrorResponse("Failed to retrieve orders", 500);
            }
        }

        [HttpGet("recent")]
        [ProducesResponseType(typeof(AntdOrderListResponse), 200)]
        public async Task<IActionResult> GetRecentOrders([FromQuery] int limit = 10)
        {
            try
            {
                var response = await _orderService.GetRecentOrdersAsync(limit);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recent Antd orders");
                return ErrorResponse("Failed to retrieve recent orders", 500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AntdOrderResponse), 200)]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var response = await _orderService.GetByIdAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Antd order {OrderId}", id);
                return ErrorResponse("Failed to retrieve order", 500);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AntdOrderCreateResponse), 201)]
        public async Task<IActionResult> CreateOrder([FromBody] AntdOrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _orderService.CreateAsync(orderDto);
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Antd order");
                return ErrorResponse("Failed to create order", 500);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AntdOrderUpdateResponse), 200)]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] AntdOrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var response = await _orderService.UpdateAsync(id, orderDto);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Antd order {OrderId}", id);
                return ErrorResponse("Failed to update order", 500);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AntdOrderDeleteResponse), 200)]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            try
            {
                var response = await _orderService.DeleteAsync(id);
                if (!response.Success) return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Antd order {OrderId}", id);
                return ErrorResponse("Failed to delete order", 500);
            }
        }
    }
}
