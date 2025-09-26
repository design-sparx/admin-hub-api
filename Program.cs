using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AdminHubApi.Data;
using AdminHubApi.Data.Seeders;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using AdminHubApi.Interfaces.Mantine;
using AdminHubApi.Repositories;
using AdminHubApi.Security;
using AdminHubApi.Security.Permissions;
using AdminHubApi.Services;
using AdminHubApi.Services.Mantine;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

    options.AddPolicy("RequireManagerRole", policy =>
        policy.RequireRole(RoleSeeder.ManagerRole, RoleSeeder.AdminRole));

    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole(RoleSeeder.UserRole, RoleSeeder.ManagerRole, RoleSeeder.AdminRole));
});

// Services
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<IUserService, UserService>();

// Mantine Dashboard Services
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<ISalesService, SalesService>();

// Repository
builder.Services.AddScoped<ITokenBlacklistRepository, TokenBlacklistRepository>();

// Register token cleanup background service
builder.Services.AddHostedService<TokenCleanupService>();

// Add permission message service
builder.Services.AddSingleton<IPermissionMessageService, PermissionMessageService>();

// Register the custom authorization handler
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
        db.Database.Migrate(); // This applies to pending migrations automatically
        logger.LogInformation("Database migrations applied successfully");

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
            await ManagerUserSeeder.SeedManagerUserAsync(app.Services);
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

        logger.LogInformation("Database seeding completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating or seeding the database");

        throw; // Rethrow to stop application startup if database setup fails
    }
}

// Configure the HTTP request pipeline.
app.MapScalarApiReference();
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();