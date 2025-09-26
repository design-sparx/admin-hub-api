using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Mantine;

namespace AdminHubApi.Interfaces.Mantine
{
    public interface IProjectService
    {
        Task<ApiResponse<List<ProjectDto>>> GetAllAsync(ProjectQueryParams queryParams);
        Task<ProjectDto?> GetByIdAsync(string id);
        Task<ProjectDto> CreateAsync(ProjectDto projectDto);
        Task<ProjectDto?> UpdateAsync(string id, ProjectDto projectDto);
        Task<bool> DeleteAsync(string id);
    }
}