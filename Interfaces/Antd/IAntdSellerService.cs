using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdSellerService
    {
        Task<AntdSellerListResponse> GetAllAsync(AntdSellerQueryParams queryParams);
        Task<AntdSellerResponse> GetByIdAsync(string id);
        Task<AntdSellerListResponse> GetTopSellersAsync(int limit);
        Task<AntdSellerCreateResponse> CreateAsync(AntdSellerDto sellerDto);
        Task<AntdSellerUpdateResponse> UpdateAsync(string id, AntdSellerDto sellerDto);
        Task<AntdSellerDeleteResponse> DeleteAsync(string id);
    }
}
