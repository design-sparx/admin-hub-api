using Microsoft.AspNetCore.Identity;

namespace AdminHubApi;

public class AppUser : IdentityUser
{
    public List<AppUserProject> AppUserProjects { get; set; } = new List<AppUserProject>();
}