using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdTruckService
    {
        Task<AntdTruckListResponse> GetAllAsync(AntdTruckQueryParams queryParams);
        Task<AntdTruckDto> GetByIdAsync(int id);
        Task<AntdTruckDto> CreateAsync(AntdTruckCreateDto createDto);
        Task<AntdTruckDto> UpdateAsync(int id, AntdTruckUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
