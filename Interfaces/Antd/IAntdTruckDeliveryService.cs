using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdTruckDeliveryService
    {
        Task<AntdTruckDeliveryListResponse> GetAllAsync(AntdTruckDeliveryQueryParams queryParams);
        Task<AntdTruckDeliveryDto> GetByIdAsync(int id);
        Task<AntdTruckDeliveryDto> CreateAsync(AntdTruckDeliveryCreateDto createDto);
        Task<AntdTruckDeliveryDto> UpdateAsync(int id, AntdTruckDeliveryUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
