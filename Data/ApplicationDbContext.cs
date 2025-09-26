using AdminHubApi.Entities;
using AdminHubApi.Entities.Mantine;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }

    // Mantine Dashboard Entities
    public DbSet<DashboardStats> DashboardStats { get; set; }
    public DbSet<Sales> Sales { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure BlacklistedToken entity
        builder.Entity<BlacklistedToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TokenId).IsUnique();
            entity.HasIndex(e => e.ExpiryDate); // For efficient cleanup queries
        });

        // Configure Mantine entities
        builder.Entity<DashboardStats>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.CreatedAt);
        });

        builder.Entity<Sales>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Source);
            entity.HasIndex(e => e.CreatedAt);
        });

        builder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Date);
            entity.HasIndex(e => e.PaymentMethod);
        });

        builder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.State);
            entity.HasIndex(e => e.Assignee);
            entity.HasIndex(e => new { e.StartDate, e.EndDate });
        });
    }
}