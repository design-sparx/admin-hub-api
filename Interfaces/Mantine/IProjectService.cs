using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IProjectService
    {
        Task<ProjectListResponse> GetAllAsync(ProjectQueryParams queryParams);
        Task<ProjectResponse> GetByIdAsync(string id);
        Task<ProjectCreateResponse> CreateAsync(ProjectDto projectDto);
        Task<ProjectUpdateResponse> UpdateAsync(string id, ProjectDto projectDto);
        Task<ProjectDeleteResponse> DeleteAsync(string id);
    }
}