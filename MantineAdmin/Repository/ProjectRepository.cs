using MantineAdmin.Data;
using MantineAdmin.Dtos.Project;
using MantineAdmin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MantineAdmin.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDBContext _context;

    public ProjectRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.Include(c => c.Comments).ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Project> CreateAsync(Project projectModel)
    {
        await _context.Projects.AddAsync(projectModel);
        await _context.SaveChangesAsync();

        return projectModel;
    }

    public async Task<Project?> UpdateAsync(int id, UpdateProjectRequestDto projectDto)
    {
        var existingProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

        if (existingProject == null)
        {
            return null;
        }

        existingProject.Name = projectDto.Name;
        existingProject.Description = projectDto.Description;
        existingProject.CreatedAt = projectDto.CreatedAt;

        await _context.SaveChangesAsync();

        return existingProject;
    }

    public async Task<Project?> DeleteAsync(int id)
    {
        var projectModel = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

        if (projectModel == null)
        {
            return null;
        }

        _context.Projects.Remove(projectModel);
        await _context.SaveChangesAsync();

        return projectModel;
    }

    public Task<bool> ProjectExists(int id)
    {
        return _context.Projects.AnyAsync(x => x.Id == id);
    }
}