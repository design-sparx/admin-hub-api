using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdClientService
    {
        Task<AntdClientListResponse> GetAllAsync(AntdClientQueryParams queryParams);
        Task<AntdClientResponse> GetByIdAsync(string id);
        Task<AntdClientCreateResponse> CreateAsync(AntdClientDto clientDto);
        Task<AntdClientUpdateResponse> UpdateAsync(string id, AntdClientDto clientDto);
        Task<AntdClientDeleteResponse> DeleteAsync(string id);
    }
}
