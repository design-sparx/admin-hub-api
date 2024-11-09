namespace MantineAdmin.Interfaces;

public interface IAppUserProjectRepository
{
    Task<List<Project>> GetUserProjects(AppUser user);
    Task<AppUserProject> CreateAsync(AppUserProject appUserProject);
}