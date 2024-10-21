using MantineAdmin.Dtos.Project;

namespace MantineAdmin.Mappers;

public static class ProjectMappers
{
    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
    }
}