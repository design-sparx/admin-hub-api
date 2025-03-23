using AdminHubApi.Dtos;
using AdminHubApi.Dtos.Projects;
using AdminHubApi.Entities;

namespace AdminHubApi.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync();
    Task<ProjectResponseDto> GetProjectByIdAsync(Guid id);
    Task<Guid> AddProjectAsync(CreateProjectDto projectDto);
    Task UpdateProjectAsync(Guid id, ProjectResponseDto projectDto);
    Task DeleteProjectAsync(Guid id);
    Task<IEnumerable<ProjectResponseDto>> GetProjectsByStatusAsync(ProjectStatus status);
}