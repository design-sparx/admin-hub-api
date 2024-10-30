﻿using MantineAdmin.Dtos.Project;
using MantineAdmin.Helpers;

namespace MantineAdmin.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync(QueryObject query);
    Task<Project?> GetByIdAsync(int id);
    Task<Project> CreateAsync(Project projectModel);
    Task<Project?> UpdateAsync(int id, UpdateProjectRequestDto projectDto);
    Task<Project?> DeleteAsync(int id);
    Task<bool> ProjectExists(int id);
}