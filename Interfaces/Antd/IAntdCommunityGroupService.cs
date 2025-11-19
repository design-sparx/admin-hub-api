using AdminHubApi.Dtos.Antd;

namespace AdminHubApi.Interfaces.Antd
{
    public interface IAntdCommunityGroupService
    {
        Task<AntdCommunityGroupListResponse> GetAllAsync(AntdCommunityGroupQueryParams queryParams);
        Task<AntdCommunityGroupResponse> GetByIdAsync(string id);
        Task<AntdCommunityGroupCreateResponse> CreateAsync(AntdCommunityGroupDto dto);
        Task<AntdCommunityGroupUpdateResponse> UpdateAsync(string id, AntdCommunityGroupDto dto);
        Task<AntdCommunityGroupDeleteResponse> DeleteAsync(string id);
    }
}
