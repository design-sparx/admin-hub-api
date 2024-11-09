using AdminHubApi.Helpers;
using AdminHubApi.Dtos.ProjectComment;
using AdminHubApi.Models;

namespace AdminHubApi.Interfaces;

public interface IProjectCommentRepository
{
    Task<List<ProjectComment>> GetAllAsync(QueryObject query);
    Task<ProjectComment?> GetByIdAsync(int id);
    Task<ProjectComment> CreateAsync(ProjectComment projectComment);
    Task<ProjectComment?> UpdateAsync(int id, ProjectComment commentDto);
    Task<ProjectComment?> DeleteAsync(int id);
}