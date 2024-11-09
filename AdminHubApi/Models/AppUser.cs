using Microsoft.AspNetCore.Identity;

namespace AdminHubApi.Models;

public class AppUser : IdentityUser
{
    public List<AppUserProject> AppUserProjects { get; set; } = new List<AppUserProject>();
}