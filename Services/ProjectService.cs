using AdminHubApi.Dtos;
using AdminHubApi.Dtos.Projects;
using AdminHubApi.Entities;
using AdminHubApi.Repositories;

namespace AdminHubApi.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllProductsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects.Select(project => new ProjectResponseDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
        });
    }

    public async Task<ProjectResponseDto> GetProductByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");

        return new ProjectResponseDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
        };
    }

    public async Task<Guid> AddProductAsync(CreateProjectDto projectDto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Title = projectDto.Title,
            Description = projectDto.Description,
            Status = projectDto.Status,
        };
        
        await _projectRepository.CreateAsync(project);

        return project.Id;
    }

    public async Task UpdateProductAsync(Guid id, ProjectResponseDto projectDto)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        
        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");
        
        project.Title = projectDto.Title;
        project.Description = projectDto.Description;
        project.Status = projectDto.Status;
        
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var project = _projectRepository.GetByIdAsync(id);
        
        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");
        
        await _projectRepository.DeleteAsync(id);
    }
}