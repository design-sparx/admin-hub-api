using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdTruckDeliveryRequestService
    {
        Task<AntdTruckDeliveryRequestListResponse> GetAllAsync(AntdTruckDeliveryRequestQueryParams queryParams);
        Task<AntdTruckDeliveryRequestDto> GetByIdAsync(int id);
        Task<AntdTruckDeliveryRequestDto> CreateAsync(AntdTruckDeliveryRequestCreateDto createDto);
        Task<AntdTruckDeliveryRequestDto> UpdateAsync(int id, AntdTruckDeliveryRequestUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
