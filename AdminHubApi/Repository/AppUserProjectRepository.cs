using AdminHubApi.Data;
using AdminHubApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Repository;

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

    public async Task<AppUserProject> DeleteAsync(AppUser appUser, int projectId)
    {
        var userProjectModel =
            await _context.AppUserProjects.FirstOrDefaultAsync(x =>
                x.AppUserId == appUser.Id && x.ProjectId == projectId);

        if (userProjectModel == null)
            return null;

        _context.AppUserProjects.Remove(userProjectModel);

        await _context.SaveChangesAsync();

        return userProjectModel;
    }
}