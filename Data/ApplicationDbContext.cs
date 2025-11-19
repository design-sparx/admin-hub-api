using AdminHubApi.Entities;
using AdminHubApi.Entities.Antd;
using AdminHubApi.Entities.Mantine;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminHubApi.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Product> Products { get; set; }

    // Mantine Dashboard Entities
    public DbSet<DashboardStats> DashboardStats { get; set; }
    public DbSet<Sales> Sales { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Projects> Projects { get; set; }
    public DbSet<Invoices> Invoices { get; set; }
    public DbSet<KanbanTasks> KanbanTasks { get; set; }
    public DbSet<UserProfiles> UserProfiles { get; set; }
    public DbSet<Files> Files { get; set; }
    public DbSet<Folders> Folders { get; set; }
    public DbSet<FileActivities> FileActivities { get; set; }
    public DbSet<Chats> Chats { get; set; }
    public DbSet<ChatMessages> ChatMessages { get; set; }
    public DbSet<Languages> Languages { get; set; }
    public DbSet<Countries> Countries { get; set; }
    public DbSet<Traffic> Traffic { get; set; }

    // Antd Dashboard Entities
    public DbSet<AntdProject> AntdProjects { get; set; }
    public DbSet<AntdClient> AntdClients { get; set; }
    public DbSet<AntdProduct> AntdProducts { get; set; }
    public DbSet<AntdSeller> AntdSellers { get; set; }
    public DbSet<AntdOrder> AntdOrders { get; set; }

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

        builder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Date);
            entity.HasIndex(e => e.PaymentMethod);
        });

        builder.Entity<Projects>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.State);
            entity.HasIndex(e => e.Assignee);
            entity.HasIndex(e => new { e.StartDate, e.EndDate });
        });

        // Configure new Mantine entities
        builder.Entity<Invoices>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.IssueDate);
            entity.HasIndex(e => e.Amount);
            entity.HasIndex(e => e.CreatedById);

            // Configure foreign key relationship for CreatedBy
            entity.HasOne(e => e.CreatedBy)
                  .WithMany()
                  .HasForeignKey(e => e.CreatedById)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<KanbanTasks>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Status);
        });

        builder.Entity<UserProfiles>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        builder.Entity<Files>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.FolderId);
            entity.HasIndex(e => e.OwnerId);
            entity.HasIndex(e => e.Type);
        });

        builder.Entity<Folders>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ParentId);
            entity.HasIndex(e => e.OwnerId);
        });

        builder.Entity<FileActivities>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.FileId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CreatedAt);
        });

        builder.Entity<Chats>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Type);
            entity.HasIndex(e => e.CreatedAt);
        });

        builder.Entity<ChatMessages>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ChatId);
            entity.HasIndex(e => e.SenderId);
            entity.HasIndex(e => e.CreatedAt);
        });

        builder.Entity<Languages>(entity =>
        {
            entity.HasKey(e => e.Code);
            entity.HasIndex(e => e.IsActive);
        });

        builder.Entity<Countries>(entity =>
        {
            entity.HasKey(e => e.Code);
        });

        builder.Entity<Traffic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Source);
            entity.HasIndex(e => e.Date);
        });

        // Configure Antd entities
        builder.Entity<AntdProject>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.Priority);
            entity.HasIndex(e => e.ProjectManager);
            entity.HasIndex(e => e.ClientName);
            entity.HasIndex(e => new { e.StartDate, e.EndDate });
            entity.Property(e => e.ProjectDuration).HasPrecision(18, 2);
        });

        builder.Entity<AntdClient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email);
            entity.HasIndex(e => e.Country);
            entity.HasIndex(e => e.ProductName);
            entity.HasIndex(e => e.PurchaseDate);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
        });

        builder.Entity<AntdProduct>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.IsFeatured);
            entity.HasIndex(e => e.QuantitySold);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.AverageRating).HasPrecision(3, 1);
        });

        builder.Entity<AntdSeller>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email);
            entity.HasIndex(e => e.SalesRegion);
            entity.HasIndex(e => e.Country);
            entity.Property(e => e.SalesVolume).HasPrecision(18, 2);
            entity.Property(e => e.TotalSales).HasPrecision(18, 2);
            entity.Property(e => e.CustomerSatisfaction).HasPrecision(5, 2);
        });

        builder.Entity<AntdOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.OrderDate);
            entity.HasIndex(e => e.PaymentMethod);
            entity.HasIndex(e => e.Country);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.ShippingCost).HasPrecision(18, 2);
            entity.Property(e => e.Tax).HasPrecision(18, 2);
        });

        // Configure Product entity
        builder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Sku).IsUnique();
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.CreatedAt);

            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.CompareAtPrice).HasPrecision(18, 2);
            entity.Property(e => e.CostPrice).HasPrecision(18, 2);

            // Configure foreign key relationship
            entity.HasOne(e => e.CreatedByUser)
                  .WithMany()
                  .HasForeignKey(e => e.CreatedBy)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure AuditLog entity
        builder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.EntityName);
            entity.HasIndex(e => e.EntityId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Action);
            entity.HasIndex(e => e.Timestamp);

            // Configure foreign key relationship
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}