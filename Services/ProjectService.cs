using AdminHubApi.Dtos.ApiResponse;
using AdminHubApi.Dtos.Projects;
using AdminHubApi.Dtos.UserManagement;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;

namespace AdminHubApi.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ApiResponse<IEnumerable<ProjectResponseDto>>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return new ApiResponse<IEnumerable<ProjectResponseDto>>
        {
            Succeeded = true,
            Data = projects.Select(MapToResponseDto),
            Message = "Projects retrieved",
            Errors = []
        };
    }

    public async Task<ApiResponse<ProjectResponseDto>> GetProjectByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null) throw new KeyNotFoundException($"Project with id: {id} was not found");

        return new ApiResponse<ProjectResponseDto>
        {
            Succeeded = true,
            Data = MapToResponseDto(project),
            Message = "Projects retrieved",
            Errors = []
        };
    }

    public async Task<ApiResponse<Guid>> AddProjectAsync(CreateProjectDto projectDto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Title = projectDto.Title,
            Description = projectDto.Description,
            Status = projectDto.Status,
            OwnerId = projectDto.OwnerId,
            StartDate = projectDto.StartDate,
            DueDate = projectDto.DueDate,
        };

        await _projectRepository.CreateAsync(project);

        return new ApiResponse<Guid>
        {
            Succeeded = true,
            Data = project.Id,
            Message = "Projects retrieved",
            Errors = []
        };
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

    public async Task<ApiResponse<IEnumerable<ProjectResponseDto>>> GetProjectsByStatusAsync(ProjectStatus status)
    {
        var projects = await _projectRepository.GetProjectsByStatusAsync(status);

        return new ApiResponse<IEnumerable<ProjectResponseDto>>
        {
            Succeeded = true,
            Data = projects.Select(MapToResponseDto),
            Message = "Projects retrieved",
            Errors = []
        };
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
            OwnerId = project.OwnerId,
            Owner = project.Owner != null
                ? new UserDto
                {
                    Id = project.Owner.Id,
                    UserName = project.Owner.UserName,
                    Email = project.Owner.Email,
                    PhoneNumber = project.Owner.PhoneNumber,
                    EmailConfirmed = project.Owner.EmailConfirmed,
                    PhoneNumberConfirmed = project.Owner.PhoneNumberConfirmed,
                    TwoFactorEnabled = project.Owner.TwoFactorEnabled,
                    LockoutEnabled = project.Owner.LockoutEnabled,
                    LockoutEnd = project.Owner.LockoutEnd
                }
                : null
        };
    }
}