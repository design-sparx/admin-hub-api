using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AdminHubApi.Constants;
using AdminHubApi.Data;
using AdminHubApi.Entities;
using AdminHubApi.Interfaces;
using AdminHubApi.Repositories;
using AdminHubApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                var tokenBlacklistRepository = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistRepository>();
                
                if (token != null)
                {
                    var tokenId = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                    
                    // Check if token is blacklisted
                    if (!string.IsNullOrEmpty(tokenId) && await tokenBlacklistRepository.IsTokenBlacklistedAsync(tokenId))
                    {
                        context.Fail("Token has been revoked");
                    }
                }
            },
            
            OnMessageReceived = context =>
            {
                // Extract token from query string from WebSocket connections if needed
                return Task.CompletedTask;
            }
        };
    });

// Authorization
builder.Services.AddAuthorization(options =>
{
    // Role-based policies
    options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));

    // Permission-based policies
    options.AddPolicy("CanViewUsers",
        policy => policy.RequireClaim(CustomClaimTypes.Permission, Permissions.ViewUsers));

    options.AddPolicy("CanManageUsers", policy =>
        policy.RequireClaim(CustomClaimTypes.Permission,
            Permissions.CreateUsers,
            Permissions.EditUsers,
            Permissions.DeleteUsers));

    // Combination policies
    options.AddPolicy("PremiumFeatures", policy =>
        policy.RequireClaim(CustomClaimTypes.SubscriptionLevel, "Premium", "Enterprise"));
});

// Add Claims
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();

// User service
builder.Services.AddScoped<IUserService, UserService>();

// Project service
builder.Services.AddScoped<IProjectService, ProjectService>();

// Repository
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITokenBlacklistRepository, TokenBlacklistRepository>();

// Register token cleanup background service
builder.Services.AddHostedService<TokenCleanupService>();

// Identity seeder
builder.Services.AddScoped<IdentitySeeder>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // This applies pending migrations automatically

    var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
app.MapScalarApiReference();
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();