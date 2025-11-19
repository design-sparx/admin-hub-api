using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdDeliveryAnalyticService
    {
        Task<AntdDeliveryAnalyticListResponse> GetAllAsync(AntdDeliveryAnalyticQueryParams queryParams);
        Task<AntdDeliveryAnalyticDto> GetByIdAsync(int id);
        Task<AntdDeliveryAnalyticDto> CreateAsync(AntdDeliveryAnalyticCreateDto createDto);
        Task<AntdDeliveryAnalyticDto> UpdateAsync(int id, AntdDeliveryAnalyticUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
