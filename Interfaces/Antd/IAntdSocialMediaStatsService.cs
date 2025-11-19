using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdSocialMediaStatsService
    {
        Task<AntdSocialMediaStatsListResponse> GetAllAsync(AntdSocialMediaStatsQueryParams queryParams);
        Task<AntdSocialMediaStatsResponse> GetByIdAsync(string id);
        Task<AntdSocialMediaStatsCreateResponse> CreateAsync(AntdSocialMediaStatsDto dto);
        Task<AntdSocialMediaStatsUpdateResponse> UpdateAsync(string id, AntdSocialMediaStatsDto dto);
        Task<AntdSocialMediaStatsDeleteResponse> DeleteAsync(string id);
    }
}
