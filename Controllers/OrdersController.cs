 using System.Security.Claims;
using AdminHubApi.Constants;
using AdminHubApi.Dtos.Orders;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using AdminHubApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHubApi.Controllers;

[ApiController]
[Route("/api/orders")]
[PermissionAuthorize(Permissions.Orders.View)]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrders()
    {
        var response = await _orderService.GetAllAsync();

        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById(Guid id)
    {
        var response = await _orderService.GetByIdAsync(id);

        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpPost]
    [PermissionAuthorize(Permissions.Orders.Create)]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder(CreateOrderDto createOrderDto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),

            // Include both registered user id and order-specific customer info
            CustomerId = createOrderDto.CustomerId,
            CustomerName = createOrderDto.CustomerName,
            CustomerEmail = createOrderDto.CustomerEmail,
            CustomerPhone = createOrderDto.CustomerPhone,

            Status = createOrderDto.Status,
            ShippingAddress = createOrderDto.ShippingAddress,
            BillingAddress = createOrderDto.BillingAddress,
            PaymentMethod = createOrderDto.PaymentMethod,
            Created = DateTime.UtcNow,
            CreatedById = createOrderDto.CreatedById,
            Modified = DateTime.UtcNow,
            ModifiedById = createOrderDto.CreatedById
        };

        var orderItems = createOrderDto.OrderItems.Select(item => new OrderItem
        {
            Id = Guid.NewGuid(),
            ProductId = item.ProductId,
            Quantity = item.Quantity
        }).ToList();

        var response = await _orderService.CreateAsync(order, orderItems);

        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize(Permissions.Orders.Edit)]
    public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderDto updateOrderDto)
    {
        var orderResponse = await _orderService.GetByIdAsync(id);

        if (!orderResponse.Succeeded)
        {
            return NotFound(orderResponse);
        }

        var existingOrder = orderResponse.Data;
        existingOrder.CustomerName = updateOrderDto.CustomerName;
        existingOrder.CustomerEmail = updateOrderDto.CustomerEmail;
        existingOrder.CustomerEmail = updateOrderDto.CustomerEmail;
        existingOrder.Status = updateOrderDto.Status;
        existingOrder.ShippingAddress = updateOrderDto.ShippingAddress;
        existingOrder.BillingAddress = updateOrderDto.BillingAddress;
        existingOrder.PaymentMethod = updateOrderDto.PaymentMethod;
        existingOrder.ModifiedById = updateOrderDto.ModifiedById;
        existingOrder.Modified = DateTime.UtcNow;

        await _orderService.UpdateAsync(existingOrder);

        return Ok(existingOrder);
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize(Permissions.Orders.Delete)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (!order.Succeeded)
        {
            return NotFound(order);
        }

        var deleteResponse = await _orderService.DeleteAsync(id);

        if (!deleteResponse.Succeeded)
        {
            return BadRequest(deleteResponse);
        }

        return NoContent();
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByCustomer(string customerId)
    {
        var response = await _orderService.GetByCustomerIdAsync(customerId);

        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByStatus(OrderStatus status)
    {
        var response = await _orderService.GetByStatusAsync(status);

        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    
    [HttpGet("customer-info")]
    public async Task<ActionResult<CustomerInfo>> GetCustomerInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID not found");
        }
    
        var response = await _orderService.GetCustomerInfoAsync(userId);
    
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }
    
        return Ok(response);
    }
}