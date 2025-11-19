using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdProjectService
    {
        Task<AntdProjectListResponse> GetAllAsync(AntdProjectQueryParams queryParams);
        Task<AntdProjectResponse> GetByIdAsync(string id);
        Task<AntdProjectCreateResponse> CreateAsync(AntdProjectDto projectDto);
        Task<AntdProjectUpdateResponse> UpdateAsync(string id, AntdProjectDto projectDto);
        Task<AntdProjectDeleteResponse> DeleteAsync(string id);
    }
}
