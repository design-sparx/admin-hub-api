using AdminHubApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    public DbSet<Project> Projects { get; set; }
}