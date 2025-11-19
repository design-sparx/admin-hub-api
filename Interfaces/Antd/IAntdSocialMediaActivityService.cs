using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdSocialMediaActivityService
    {
        Task<AntdSocialMediaActivityListResponse> GetAllAsync(AntdSocialMediaActivityQueryParams queryParams);
        Task<AntdSocialMediaActivityResponse> GetByIdAsync(string id);
        Task<AntdSocialMediaActivityCreateResponse> CreateAsync(AntdSocialMediaActivityDto dto);
        Task<AntdSocialMediaActivityUpdateResponse> UpdateAsync(string id, AntdSocialMediaActivityDto dto);
        Task<AntdSocialMediaActivityDeleteResponse> DeleteAsync(string id);
    }
}
