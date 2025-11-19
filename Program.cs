using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AdminHubApi.Data;
using AdminHubApi.Data.Seeders;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using AdminHubApi.Interfaces.Antd;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Repositories;
using AdminHubApi.Security;
using AdminHubApi.Security.Permissions;
using AdminHubApi.Services;
using AdminHubApi.Services.Antd;
using AdminHubApi.Services.Mantine;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Configure CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
        else
        {
            // Fallback for development if no origins configured
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    });
});

// DB Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register token service
builder.Services.AddScoped<ITokenService, TokenService>();

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Password requirements
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;

        // User settings
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };

        // Add custom token validation to check blacklist
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                var token = context.SecurityToken as JwtSecurityToken;

                var tokenBlacklistRepository =
                    context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistRepository>();

                if (token != null)
                {
                    var tokenId = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                    // Check if token is blacklisted
                    if (!string.IsNullOrEmpty(tokenId) &&
                        await tokenBlacklistRepository.IsTokenBlacklistedAsync(tokenId))
                    {
                        context.Fail("Token has been revoked");
                    }
                }
            },

            OnMessageReceived = context =>
            {
                // Extract token from a query string from WebSocket connections if needed
                return Task.CompletedTask;
            }
        };
    });

// Register the custom authorization policy provider
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole(RoleSeeder.AdminRole));

    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole(RoleSeeder.UserRole, RoleSeeder.AdminRole));
});

// Services
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Mantine Dashboard Services
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IKanbanTaskService, KanbanTaskService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IFileManagementService, FileManagementService>();
builder.Services.AddScoped<ICommunicationService, CommunicationService>();

// Antd Dashboard Services
builder.Services.AddScoped<ITaskService, TaskService>();

// Antd Dashboard Services
builder.Services.AddScoped<IAntdProjectService, AntdProjectService>();
builder.Services.AddScoped<IAntdClientService, AntdClientService>();
builder.Services.AddScoped<IAntdProductService, AntdProductService>();
builder.Services.AddScoped<IAntdSellerService, AntdSellerService>();
builder.Services.AddScoped<IAntdOrderService, AntdOrderService>();
builder.Services.AddScoped<IAntdCampaignAdService, AntdCampaignAdService>();
builder.Services.AddScoped<IAntdSocialMediaStatsService, AntdSocialMediaStatsService>();
builder.Services.AddScoped<IAntdSocialMediaActivityService, AntdSocialMediaActivityService>();
builder.Services.AddScoped<IAntdScheduledPostService, AntdScheduledPostService>();
builder.Services.AddScoped<IAntdLiveAuctionService, AntdLiveAuctionService>();
builder.Services.AddScoped<IAntdAuctionCreatorService, AntdAuctionCreatorService>();

// Repository
builder.Services.AddScoped<ITokenBlacklistRepository, TokenBlacklistRepository>();

// Register token cleanup background service
builder.Services.AddHostedService<TokenCleanupService>();

// Add permission message service
builder.Services.AddSingleton<IPermissionMessageService, PermissionMessageService>();

// Register the custom authorization handler
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "Admin Hub API",
            Version = "v1",
            Description = """
                ## Introduction

                Admin Hub API is a centralized backend service powering multiple administrative dashboards.
                Built with .NET 9.0 and PostgreSQL, it provides a robust foundation for enterprise administration.

                ### Key Features

                - **Authentication & Security**: JWT-based authentication with token refresh and blacklisting
                - **Role-Based Access Control**: Fine-grained permissions with dynamic policy enforcement
                - **Audit Logging**: Complete audit trail of all user operations
                - **Multi-Dashboard Support**: Dedicated endpoints for Mantine and Ant Design dashboards

                ### Getting Started

                1. **Authenticate**: POST to `/api/auth/login` with your credentials
                2. **Get Token**: Extract the JWT token from the response
                3. **Make Requests**: Include the token in subsequent requests

                ### Authentication

                All endpoints (except `/api/auth/login` and `/api/auth/register`) require authentication.

                Include the JWT token in the `Authorization` header:
                ```
                Authorization: Bearer <your-token>
                ```

                ### Rate Limiting

                API requests are subject to rate limiting. If you receive a 429 response, please wait before retrying.

                ### Support

                For issues or questions, please contact the development team.
                """,
            Contact = new OpenApiContact
            {
                Name = "Admin Hub Support",
                Email = "support@adminhub.dev",
                Url = new Uri("https://github.com/design-sparx/admin-hub-api")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<ILogger<Program>>()
    .LogInformation("Seeding database...");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Applying database migrations...");

        // Check if database can be connected
        if (await db.Database.CanConnectAsync())
        {
            logger.LogInformation("Database connection successful");

            // Get pending migrations
            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogInformation($"Found {pendingMigrations.Count()} pending migrations. Applying...");
                db.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully");
            }
            else
            {
                logger.LogInformation("Database is up to date. No pending migrations.");
            }
        }
        else
        {
            logger.LogError("Cannot connect to database");
            throw new Exception("Database connection failed");
        }

        // Check if we need to seed users and roles
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Check if any roles exist
        if (!await roleManager.Roles.AnyAsync())
        {
            logger.LogInformation("No roles found. Seeding roles...");
            await RoleSeeder.SeedRolesAsync(app.Services);
        }

        // Check if any users exist
        if (!await userManager.Users.AnyAsync())
        {
            logger.LogInformation("No users found. Seeding users...");

            // Add seeders
            await AdminUserSeeder.SeedAdminUserAsync(app.Services);
            await NormalUserSeeder.SeedNormalUserAsync(app.Services);
        }

        // Always update permissions to ensure new permissions are added
        logger.LogInformation("Updating role permissions...");
        await PermissionUpdateSeeder.UpdateRolePermissionsAsync(app.Services);
        logger.LogInformation("Role permissions updated successfully");

        logger.LogInformation("Updating user permissions...");
        await UserPermissionUpdateSeeder.UpdateUserPermissionsAsync(app.Services);
        logger.LogInformation("User permissions updated successfully");

        // Seed Mantine dashboard data
        logger.LogInformation("Seeding Mantine dashboard data...");
        await MantineDataSeeder.SeedMantineDataAsync(app.Services);
        logger.LogInformation("Mantine dashboard data seeded successfully");

        // Seed Antd dashboard data
        logger.LogInformation("Seeding Antd dashboard data...");
        await AntdDataSeeder.SeedAntdDataAsync(app.Services);
        logger.LogInformation("Antd dashboard data seeded successfully");

        // Seed Products
        logger.LogInformation("Seeding products...");
        await ProductSeeder.SeedProductsAsync(app.Services);
        logger.LogInformation("Products seeded successfully");

        logger.LogInformation("Database seeding completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database");

        throw; // Rethrow to stop application startup if database setup fails
    }
}

// Configure the HTTP request pipeline.
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Admin Hub API")
        .WithTheme(ScalarTheme.Kepler)
        .WithDarkModeToggle(true)
        .WithSidebar(true)
        .WithModels(true)
        .WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.Axios)
        .WithPreferredScheme("Bearer");
});
app.MapOpenApi();

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();