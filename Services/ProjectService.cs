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

    public async Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects.Select(MapToResponseDto);
    }

    public async Task<ProjectResponseDto> GetProjectByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");

        return MapToResponseDto(project);
    }

    public async Task<Guid> AddProjectAsync(CreateProjectDto projectDto)
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

    public async Task UpdateProjectAsync(Guid id, ProjectResponseDto projectDto)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        
        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");
        
        project.Title = projectDto.Title;
        project.Description = projectDto.Description;
        project.Status = projectDto.Status;
        
        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        var project = _projectRepository.GetByIdAsync(id);
        
        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");
        
        await _projectRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetProjectsByStatusAsync(ProjectStatus status)
    {
        var projects = await _projectRepository.GetProjectsByStatusAsync(status);

        return projects.Select(MapToResponseDto);
    }

    private static ProjectResponseDto MapToResponseDto(Project project)
    {
        return new ProjectResponseDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
            CreatedDate = project.CreatedDate,
            StartDate = project.StartDate,
            DueDate = project.DueDate,
            CompletedDate = project.CompletedDate,
        };
    }
}