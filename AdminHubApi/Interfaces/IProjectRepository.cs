using AdminHubApi.Dtos.Project;
using AdminHubApi.Helpers;
using AdminHubApi.Models;

namespace AdminHubApi.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync(QueryObject query);
    Task<Project?> GetByIdAsync(int id);
    Task<Project> CreateAsync(Project projectModel);
    Task<Project?> UpdateAsync(int id, UpdateProjectRequestDto projectDto);
    Task<Project?> DeleteAsync(int id);
    Task<bool> ProjectExists(int id);
}