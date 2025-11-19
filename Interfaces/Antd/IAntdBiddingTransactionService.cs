using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdBiddingTransactionService
    {
        Task<AntdBiddingTransactionListResponse> GetAllAsync(AntdBiddingTransactionQueryParams queryParams);
        Task<AntdBiddingTransactionResponse> GetByIdAsync(string id);
        Task<AntdBiddingTransactionCreateResponse> CreateAsync(AntdBiddingTransactionDto dto);
        Task<AntdBiddingTransactionUpdateResponse> UpdateAsync(string id, AntdBiddingTransactionDto dto);
        Task<AntdBiddingTransactionDeleteResponse> DeleteAsync(string id);
    }
}
