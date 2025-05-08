using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Orders;
using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IOrderService
{
    Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllAsync();
    Task<ApiResponse<OrderResponseDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetByCustomerIdAsync(string customerId);
    Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetByStatusAsync(OrderStatus status);
    Task<ApiResponse<OrderResponseDto>> CreateAsync(Order order, List<OrderItem> orderItems);
    Task<ApiResponse<OrderResponseDto>> UpdateAsync(OrderResponseDto orderResponseDto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
    Task<ApiResponse<CustomerInfo>> GetCustomerInfoAsync(string customerId);
}