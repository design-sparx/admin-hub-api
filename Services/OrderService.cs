using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.OrderItem;
using AdminHubApi.Dtos.Orders;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<OrderService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IProductRepository productRepository,
        UserManager<ApplicationUser> userManager,
        ILogger<OrderService> logger
    )
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllAsync()
    {
        try
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderResponseDtos = orders.Select(MapToOrderResponseDto);

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Succeeded = true,
                Data = orderResponseDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all orders");

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Succeeded = false,
                Message = "Error getting all orders",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<OrderResponseDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return new ApiResponse<OrderResponseDto>
                {
                    Succeeded = false,
                    Message = $"Order with ID {id} not found",
                    Errors = new List<string> { $"Order with ID {id} not found" }
                };
            }

            return new ApiResponse<OrderResponseDto>
            {
                Succeeded = true,
                Data = MapToOrderResponseDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting order by id {Id}", id);

            return new ApiResponse<OrderResponseDto>
            {
                Succeeded = false,
                Message = $"Error getting order by id {id}",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetByCustomerIdAsync(string customerId)
    {
        try
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
            var orderResponseDtos = orders.Select(MapToOrderResponseDto);

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Succeeded = true,
                Data = orderResponseDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting orders for customer {CustomerId}", customerId);

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Succeeded = false,
                Message = $"Error getting orders for customer {customerId}",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetByStatusAsync(OrderStatus status)
    {
        try
        {
            var orders = await _orderRepository.GetByStatusAsync(status);
            var orderResponseDtos = orders.Select(MapToOrderResponseDto);

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Succeeded = true,
                Data = orderResponseDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting orders by status {Status}", status);

            return new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Succeeded = false,
                Message = $"Error getting orders by status {status}",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<OrderResponseDto>> CreateAsync(Order order, List<OrderItem> orderItems)
    {
        try
        {
            decimal totalAmount = 0;

            foreach (var item in orderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                {
                    return new ApiResponse<OrderResponseDto>
                    {
                        Succeeded = false,
                        Message = $"Product with ID {item.ProductId} not found",
                        Errors = new List<string> { $"Product with ID {item.ProductId} not found" }
                    };
                }

                if (product.QuantityInStock < item.Quantity)
                {
                    return new ApiResponse<OrderResponseDto>
                    {
                        Succeeded = false,
                        Message = $"Not enough stock for product {product.Title}",
                        Errors = new List<string> { $"Not enough stock for product {product.Title}" }
                    };
                }

                item.UnitPrice = product.Price;
                item.Subtotal = item.UnitPrice * item.Quantity;
                item.Created = DateTime.UtcNow;
                item.Modified = DateTime.UtcNow;
                item.CreatedById = order.CreatedById;
                item.ModifiedById = order.ModifiedById;

                totalAmount += item.Subtotal;

                // Update product stock
                product.QuantityInStock -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            order.TotalAmount = totalAmount;
            order.OrderDate = DateTime.UtcNow;

            var createdOrder = await _orderRepository.CreateAsync(order);

            foreach (var item in orderItems)
            {
                item.OrderId = createdOrder.Id;
                await _orderItemRepository.CreateAsync(item);
            }

            // Retrieve the complete order with items
            var completeOrder = await _orderRepository.GetByIdAsync(createdOrder.Id);

            return new ApiResponse<OrderResponseDto>
            {
                Succeeded = true,
                Data = MapToOrderResponseDto(completeOrder!)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");

            return new ApiResponse<OrderResponseDto>
            {
                Succeeded = false,
                Message = "Error creating order",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<OrderResponseDto>> UpdateAsync(Order order)
    {
        try
        {
            await _orderRepository.UpdateAsync(order);

            var updatedOrder = await _orderRepository.GetByIdAsync(order.Id);

            return new ApiResponse<OrderResponseDto>
            {
                Succeeded = true,
                Data = MapToOrderResponseDto(updatedOrder!)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order {Id}", order.Id);

            return new ApiResponse<OrderResponseDto>
            {
                Succeeded = false,
                Message = $"Error updating order {order.Id}",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        try
        {
            await _orderRepository.DeleteAsync(id);

            return new ApiResponse<bool>
            {
                Succeeded = true,
                Data = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting order {Id}", id);

            return new ApiResponse<bool>
            {
                Succeeded = false,
                Message = $"Error deleting order {id}",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<CustomerInfo>> GetCustomerInfoAsync(string customerId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(customerId);

            if (user == null)
            {
                return new ApiResponse<CustomerInfo>
                {
                    Succeeded = false,
                    Message = $"User with ID {customerId} not found",
                    Errors = new List<string> { $"User with ID {customerId} not found" }
                };
            }

            return new ApiResponse<CustomerInfo>
            {
                Succeeded = true,
                Data = new CustomerInfo
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customer info for user {CustomerId}", customerId);

            return new ApiResponse<CustomerInfo>
            {
                Succeeded = false,
                Message = $"Error getting customer info for user {customerId}",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    private static OrderResponseDto MapToOrderResponseDto(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            // Include both registered user info and order-specific customer info
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            CustomerEmail = order.CustomerEmail,
            CustomerPhone = order.CustomerPhone,

            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            ShippingAddress = order.ShippingAddress,
            BillingAddress = order.BillingAddress,
            PaymentMethod = order.PaymentMethod,
            OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Title,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                Subtotal = oi.Subtotal,
                Created = oi.Created,
                Modified = oi.Modified
            }).ToList(),
            Created = order.Created,
            CreatedById = order.CreatedById,
            CreatedByName = order.CreatedBy?.UserName,
            Modified = order.Modified,
            ModifiedById = order.ModifiedById,
            ModifiedByName = order.ModifiedBy?.UserName
        };
    }
}