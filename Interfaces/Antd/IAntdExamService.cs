using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdExamService
    {
        Task<AntdExamListResponse> GetAllAsync(AntdExamQueryParams queryParams);
        Task<AntdExamResponse> GetByIdAsync(string id);
        Task<AntdExamCreateResponse> CreateAsync(AntdExamDto dto);
        Task<AntdExamUpdateResponse> UpdateAsync(string id, AntdExamDto dto);
        Task<AntdExamDeleteResponse> DeleteAsync(string id);
    }
}
