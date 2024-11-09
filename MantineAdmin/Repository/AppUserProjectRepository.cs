using MantineAdmin.Data;
using MantineAdmin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MantineAdmin.Repository;

public class AppUserProjectRepository : IAppUserProjectRepository
{
    private readonly ApplicationDBContext _context;

    public AppUserProjectRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetUserProjects(AppUser user)
    {
        return await _context.AppUserProjects
            .Where(u => u.AppUserId == user.Id)
            .Select(project => new Project
            {
                Id = project.ProjectId,
                Name = project.Project.Name,
                CreatedAt = project.Project.CreatedAt,
                Description = project.Project.Description,
            }).ToListAsync();
    }

    public async Task<AppUserProject> CreateAsync(AppUserProject appUserProject)
    {
        await _context.AppUserProjects.AddAsync(appUserProject);
        await _context.SaveChangesAsync();

        return appUserProject;
    }
}