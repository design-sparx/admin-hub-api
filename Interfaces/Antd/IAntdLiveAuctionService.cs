using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdLiveAuctionService
    {
        Task<AntdLiveAuctionListResponse> GetAllAsync(AntdLiveAuctionQueryParams queryParams);
        Task<AntdLiveAuctionResponse> GetByIdAsync(string id);
        Task<AntdLiveAuctionCreateResponse> CreateAsync(AntdLiveAuctionDto dto);
        Task<AntdLiveAuctionUpdateResponse> UpdateAsync(string id, AntdLiveAuctionDto dto);
        Task<AntdLiveAuctionDeleteResponse> DeleteAsync(string id);
    }
}
