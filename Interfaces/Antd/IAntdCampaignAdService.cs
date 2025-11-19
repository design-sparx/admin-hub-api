using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdCampaignAdService
    {
        Task<AntdCampaignAdListResponse> GetAllAsync(AntdCampaignAdQueryParams queryParams);
        Task<AntdCampaignAdResponse> GetByIdAsync(string id);
        Task<AntdCampaignAdCreateResponse> CreateAsync(AntdCampaignAdDto campaignAdDto);
        Task<AntdCampaignAdUpdateResponse> UpdateAsync(string id, AntdCampaignAdDto campaignAdDto);
        Task<AntdCampaignAdDeleteResponse> DeleteAsync(string id);
    }
}
