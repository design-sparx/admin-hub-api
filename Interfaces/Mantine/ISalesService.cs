using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface ISalesService
    {
        Task<ApiResponse<List<SalesDto>>> GetAllAsync(SalesQueryParams queryParams);
        Task<SalesDto?> GetByIdAsync(int id);
        Task<SalesDto> CreateAsync(SalesDto salesDto);
        Task<SalesDto?> UpdateAsync(int id, SalesDto salesDto);
        Task<bool> DeleteAsync(int id);
    }
}