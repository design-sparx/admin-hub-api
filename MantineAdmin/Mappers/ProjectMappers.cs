using MantineAdmin.Dtos.Project;

namespace MantineAdmin.Mappers;

public static class ProjectMappers
{
    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
    }

    public static Project ToProjectFromCreateDto(this CreateProjectRequestDto projectDto)
    {
        return new Project
        {
            Name = projectDto.Name,
            Description = projectDto.Description,
        };
    }
}