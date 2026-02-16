using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdFaqService
    {
        Task<AntdFaqListResponse> GetAllAsync(AntdFaqQueryParams queryParams);
        Task<AntdFaqResponse> GetByIdAsync(string id);
        Task<AntdFaqStatisticsResponse> GetStatisticsAsync();
        Task<AntdFaqListResponse> GetFeaturedAsync(int limit = 10);
        Task<AntdFaqCreateResponse> CreateAsync(AntdFaqDto faqDto);
        Task<AntdFaqUpdateResponse> UpdateAsync(string id, AntdFaqDto faqDto);
        Task<AntdFaqDeleteResponse> DeleteAsync(string id);
    }
}
