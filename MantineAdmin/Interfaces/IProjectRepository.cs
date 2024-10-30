using MantineAdmin.Dtos.Project;

namespace MantineAdmin.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project> CreateAsync(Project projectModel);
    Task<Project?> UpdateAsync(int id, UpdateProjectRequestDto projectDto);
    Task<Project?> DeleteAsync(int id);
}