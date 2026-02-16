using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdEmployeeService
    {
        Task<AntdEmployeeListResponse> GetAllAsync(AntdEmployeeQueryParams queryParams);
        Task<AntdEmployeeResponse> GetByIdAsync(string id);
        Task<AntdEmployeeStatisticsResponse> GetStatisticsAsync();
        Task<AntdEmployeeCreateResponse> CreateAsync(AntdEmployeeDto employeeDto);
        Task<AntdEmployeeUpdateResponse> UpdateAsync(string id, AntdEmployeeDto employeeDto);
        Task<AntdEmployeeDeleteResponse> DeleteAsync(string id);
    }
}
