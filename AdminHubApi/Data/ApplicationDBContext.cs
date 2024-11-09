using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }
    public DbSet<AppUserProject> AppUserProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUserProject>(x => x.HasKey(a => new { a.AppUserId, a.ProjectId }));
        
        modelBuilder.Entity<AppUserProject>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.AppUserProjects)
            .HasForeignKey(a => a.AppUserId);
        
        modelBuilder.Entity<AppUserProject>()
            .HasOne(u => u.Project)
            .WithMany(u => u.AppUserProjects)
            .HasForeignKey(a => a.ProjectId);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}