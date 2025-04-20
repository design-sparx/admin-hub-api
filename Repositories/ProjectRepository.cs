using AdminHubApi.Data;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _dbContext.Projects.ToListAsync();
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        return await _dbContext.Projects.FindAsync(id);
    }

    public async Task CreateAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        _dbContext.Projects.Update(project);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var projectToDelete = await _dbContext.Projects.FindAsync(id);

        if (projectToDelete != null)
        {
            _dbContext.Projects.Remove(projectToDelete);
            
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(ProjectStatus status)
    {
        return await _dbContext.Projects.Where(p => p.Status == status).ToListAsync();
    }
}