using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdScheduledPostService
    {
        Task<AntdScheduledPostListResponse> GetAllAsync(AntdScheduledPostQueryParams queryParams);
        Task<AntdScheduledPostResponse> GetByIdAsync(string id);
        Task<AntdScheduledPostCreateResponse> CreateAsync(AntdScheduledPostDto dto);
        Task<AntdScheduledPostUpdateResponse> UpdateAsync(string id, AntdScheduledPostDto dto);
        Task<AntdScheduledPostDeleteResponse> DeleteAsync(string id);
    }
}
