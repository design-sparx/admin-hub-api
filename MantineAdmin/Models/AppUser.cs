using Microsoft.AspNetCore.Identity;

namespace MantineAdmin;

public class AppUser : IdentityUser
{
    public List<AppUserProject> AppUserProjects { get; set; } = new List<AppUserProject>();
}