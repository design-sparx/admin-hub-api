using MantineAdmin.Dtos.ProjectComment;

namespace MantineAdmin.Mappers;

public static class ProjectCommentMapper
{
    public static ProjectCommentDto ToProjectCommentDto(this ProjectComment projectCommentModel)
    {
        return new ProjectCommentDto
        {
            Id = projectCommentModel.Id,
            Title = projectCommentModel.Title,
            CreatedAt = projectCommentModel.CreatedAt,
            Content = projectCommentModel.Content,
            ProjectId = projectCommentModel.ProjectId,
        };
    }

    public static ProjectComment ToCommentFromCreate(this CreateProjectCommentDto commentDto, int projectId)
    {
        return new ProjectComment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            ProjectId = projectId
        };
    }
    
    public static ProjectComment ToCommentFromUpdate(this UpdateProjectCommentDto commentDto, int projectId)
    {
        return new ProjectComment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            ProjectId = projectId
        };
    }
}