using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdPricingService
    {
        Task<AntdPricingListResponse> GetAllAsync(AntdPricingQueryParams queryParams);
        Task<AntdPricingResponse> GetByIdAsync(string id);
        Task<AntdPricingResponse> GetByPlanAsync(string plan);
        Task<AntdPricingCreateResponse> CreateAsync(AntdPricingDto pricingDto);
        Task<AntdPricingUpdateResponse> UpdateAsync(string id, AntdPricingDto pricingDto);
        Task<AntdPricingDeleteResponse> DeleteAsync(string id);
    }
}
