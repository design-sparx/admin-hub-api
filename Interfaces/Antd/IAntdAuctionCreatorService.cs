using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdAuctionCreatorService
    {
        Task<AntdAuctionCreatorListResponse> GetAllAsync(AntdAuctionCreatorQueryParams queryParams);
        Task<AntdAuctionCreatorResponse> GetByIdAsync(string id);
        Task<AntdAuctionCreatorCreateResponse> CreateAsync(AntdAuctionCreatorDto dto);
        Task<AntdAuctionCreatorUpdateResponse> UpdateAsync(string id, AntdAuctionCreatorDto dto);
        Task<AntdAuctionCreatorDeleteResponse> DeleteAsync(string id);
    }
}
