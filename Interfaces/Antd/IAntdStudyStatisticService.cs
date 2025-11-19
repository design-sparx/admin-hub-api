using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdStudyStatisticService
    {
        Task<AntdStudyStatisticListResponse> GetAllAsync(AntdStudyStatisticQueryParams queryParams);
        Task<AntdStudyStatisticResponse> GetByIdAsync(string id);
        Task<AntdStudyStatisticCreateResponse> CreateAsync(AntdStudyStatisticDto dto);
        Task<AntdStudyStatisticUpdateResponse> UpdateAsync(string id, AntdStudyStatisticDto dto);
        Task<AntdStudyStatisticDeleteResponse> DeleteAsync(string id);
    }
}
