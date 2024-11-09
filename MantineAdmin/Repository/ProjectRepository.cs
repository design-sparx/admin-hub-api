using MantineAdmin.Data;
using MantineAdmin.Dtos.Project;
using MantineAdmin.Helpers;
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

    public async Task<List<Project>> GetAllAsync(QueryObject query)
    {
        var projects = _context.Projects.Include(c => c.Comments).ThenInclude(p => p.AppUser).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            projects = projects.Where(s => s.Name.Contains(query.Name));
        }

        // if (!string.IsNullOrWhiteSpace(query.SortBy))
        // {
        //     if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
        //     {
        //         projects = query.IsDescending ? projects.OrderByDescending(p => p.Name) : projects.OrderBy(p => p.Name);
        //     }
        // }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;


        return await projects.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects.Include(c => c.Comments).ThenInclude(p => p.AppUser).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Project> CreateAsync(Project projectModel)
    {
        await _context.Projects.AddAsync(projectModel);
        await _context.SaveChangesAsync();

        return projectModel;
    }

    public async Task<Project?> UpdateAsync(int id, UpdateProjectRequestDto projectDto)
    {
        var existingProject = await _context.Projects.FindAsync(id);

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