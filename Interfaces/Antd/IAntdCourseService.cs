using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdCourseService
    {
        Task<AntdCourseListResponse> GetAllAsync(AntdCourseQueryParams queryParams);
        Task<AntdCourseResponse> GetByIdAsync(string id);
        Task<AntdCourseCreateResponse> CreateAsync(AntdCourseDto dto);
        Task<AntdCourseUpdateResponse> UpdateAsync(string id, AntdCourseDto dto);
        Task<AntdCourseDeleteResponse> DeleteAsync(string id);
    }
}
