using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdOrderService
    {
        Task<AntdOrderListResponse> GetAllAsync(AntdOrderQueryParams queryParams);
        Task<AntdOrderResponse> GetByIdAsync(string id);
        Task<AntdOrderListResponse> GetRecentOrdersAsync(int limit);
        Task<AntdOrderCreateResponse> CreateAsync(AntdOrderDto orderDto);
        Task<AntdOrderUpdateResponse> UpdateAsync(string id, AntdOrderDto orderDto);
        Task<AntdOrderDeleteResponse> DeleteAsync(string id);
    }
}
