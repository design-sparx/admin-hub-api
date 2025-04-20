using AdminHubApi.Entities;

namespace AdminHubApi.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project> GetByIdAsync(Guid id);
    Task CreateAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status);
}