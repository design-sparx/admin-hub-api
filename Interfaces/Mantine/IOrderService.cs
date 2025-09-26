using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IOrderService
    {
        Task<ApiResponse<List<OrderDto>>> GetAllAsync(OrderQueryParams queryParams);
        Task<OrderDto?> GetByIdAsync(string id);
        Task<OrderDto> CreateAsync(OrderDto orderDto);
        Task<OrderDto?> UpdateAsync(string id, OrderDto orderDto);
        Task<bool> DeleteAsync(string id);
    }
}