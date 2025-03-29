using AdminHubApi.Dtos;
using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Projects;
using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IProjectService
{
    Task<ApiResponse<IEnumerable<ProjectResponseDto>>> GetAllProjectsAsync();
    Task<ApiResponse<ProjectResponseDto>> GetProjectByIdAsync(Guid id);
    Task<ApiResponse<Guid>> AddProjectAsync(CreateProjectDto projectDto);
    Task UpdateProjectAsync(Guid id, ProjectResponseDto projectDto);
    Task DeleteProjectAsync(Guid id);
    Task<ApiResponse<IEnumerable<ProjectResponseDto>>> GetProjectsByStatusAsync(ProjectStatus status);
}