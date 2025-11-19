using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdRecommendedCourseService
    {
        Task<AntdRecommendedCourseListResponse> GetAllAsync(AntdRecommendedCourseQueryParams queryParams);
        Task<AntdRecommendedCourseResponse> GetByIdAsync(string id);
        Task<AntdRecommendedCourseCreateResponse> CreateAsync(AntdRecommendedCourseDto dto);
        Task<AntdRecommendedCourseUpdateResponse> UpdateAsync(string id, AntdRecommendedCourseDto dto);
        Task<AntdRecommendedCourseDeleteResponse> DeleteAsync(string id);
    }
}
