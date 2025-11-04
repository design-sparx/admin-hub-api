using AdminHubApi.Constants;
using AdminHubApi.Dtos.Mantine;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers.Mantine
{
    [Route("/api/v1/mantine/orders")]
    [Tags("Mantine - Orders")]
    [PermissionAuthorize(Permissions.Team.Orders)]
    public class OrdersController : MantineBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
            : base(logger)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders with pagination and filtering
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderQueryParams queryParams)
        {
            try
            {
                var orders = await _orderService.GetAllAsync(queryParams);
                return Ok(new
                {
                    success = true,
                    data = orders.Data,
                    message = "Orders retrieved successfully",
                    timestamp = DateTime.UtcNow,
                    meta = orders.Meta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orders");
                return ErrorResponse("Failed to retrieve orders", 500);
            }
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null)
                    return NotFound(new { success = false, message = "Order not found" });

                return SuccessResponse(order, "Order retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order {OrderId}", id);
                return ErrorResponse("Failed to retrieve order", 500);
            }
        }

        /// <summary>
        /// Create new order
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var order = await _orderService.CreateAsync(orderDto);
                return SuccessResponse(order, "Order created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return ErrorResponse("Failed to create order", 500);
            }
        }

        /// <summary>
        /// Update existing order
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] OrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var order = await _orderService.UpdateAsync(id, orderDto);
                if (order == null)
                    return NotFound(new { success = false, message = "Order not found" });

                return SuccessResponse(order, "Order updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order {OrderId}", id);
                return ErrorResponse("Failed to update order", 500);
            }
        }

        /// <summary>
        /// Delete order
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            try
            {
                var deleted = await _orderService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { success = false, message = "Order not found" });

                return SuccessResponse(new { }, "Order deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order {OrderId}", id);
                return ErrorResponse("Failed to delete order", 500);
            }
        }
    }
}