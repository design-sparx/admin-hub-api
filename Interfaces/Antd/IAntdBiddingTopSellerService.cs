using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdBiddingTopSellerService
    {
        Task<AntdBiddingTopSellerListResponse> GetAllAsync(AntdBiddingTopSellerQueryParams queryParams);
        Task<AntdBiddingTopSellerResponse> GetByIdAsync(string id);
        Task<AntdBiddingTopSellerCreateResponse> CreateAsync(AntdBiddingTopSellerDto dto);
        Task<AntdBiddingTopSellerUpdateResponse> UpdateAsync(string id, AntdBiddingTopSellerDto dto);
        Task<AntdBiddingTopSellerDeleteResponse> DeleteAsync(string id);
    }
}
