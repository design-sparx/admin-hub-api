﻿using AdminHubApi.Dtos.Project;
using AdminHubApi.Models;

namespace AdminHubApi.Mappers;

public static class ProjectMapper
{
    public static ProjectDto ToProjectDto(this Project projectModel)
    {
        return new ProjectDto
        {
            Id = projectModel.Id,
            Name = projectModel.Name,
            Description = projectModel.Description,
            CreatedAt = projectModel.CreatedAt,
            Comments = projectModel.Comments.Select(c => c.ToProjectCommentDto()).ToList(),
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