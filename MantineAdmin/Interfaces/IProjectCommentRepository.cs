﻿using MantineAdmin.Dtos.ProjectComment;

namespace MantineAdmin.Interfaces;

public interface IProjectCommentRepository
{
    Task<List<ProjectComment>> GetAllAsync();
    Task<ProjectComment?> GetByIdAsync(int id);
    Task<ProjectComment> CreateAsync(ProjectComment projectComment);
    Task<ProjectComment?> UpdateAsync(int id, ProjectComment commentDto);
    Task<ProjectComment?> DeleteAsync(int id);
}