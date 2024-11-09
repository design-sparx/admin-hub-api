using AdminHubApi.Dtos.ProjectComment;
using AdminHubApi.Models;

namespace AdminHubApi.Mappers;

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
            CreatedBy = projectCommentModel.AppUser.UserName,
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