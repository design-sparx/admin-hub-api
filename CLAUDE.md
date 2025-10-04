# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Admin Hub API is a centralized ASP.NET Web API backend service powering multiple administrative dashboards. Built with .NET 9.0 and PostgreSQL, it provides authentication, RBAC, audit logging, and RESTful endpoints for frontend applications.

## Common Commands

### Development
```bash
# Run with hot reload
dotnet watch run

# Build the project
dotnet build

# Run the application
dotnet run
```

### Database Migrations
```bash
# Create a new migration
dotnet ef migrations add <MigrationName>

# Apply migrations to database
dotnet ef database update

# Rollback to specific migration
dotnet ef database update <MigrationName>

# Remove last migration (if not applied)
dotnet ef migrations remove
```

### Configuration
Before running, update `appsettings.json` with your PostgreSQL connection string (note: the project uses PostgreSQL via Npgsql, not SQL Server):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=AdminHubDb;Username=youruser;Password=yourpassword"
  }
}
```

## Architecture

### Project Structure

```
Controllers/
├── Common/        # Shared controllers (Auth, Users, Profile, Audit, Languages, Countries)
├── Mantine/       # Mantine dashboard-specific controllers
└── Antd/          # Ant Design dashboard-specific controllers

Data/
├── ApplicationDbContext.cs
└── Seeders/       # Database seeders for roles, users, permissions, and Mantine data

Entities/          # Database models
├── ApplicationUser.cs
├── AuditLog.cs
├── BlacklistedToken.cs
└── Mantine/       # Mantine dashboard entities

Services/          # Business logic implementations
├── Common services (UserService, AuditService, TokenService, etc.)
└── Mantine/       # Mantine dashboard services

Interfaces/        # Service contracts
└── Mantine/       # Mantine dashboard interfaces

Repositories/      # Data access layer
Security/          # Authorization and permission infrastructure
Dtos/              # Data transfer objects
```

### Startup Flow (Program.cs)

1. **Service Registration**: DbContext (PostgreSQL), Identity, JWT authentication, CORS, services, repositories
2. **Authentication**: JWT-based with custom token validation checking a token blacklist
3. **Authorization**: Dynamic permission-based policies via `PermissionAuthorizationPolicyProvider`
4. **Database Initialization**: On startup, automatically:
   - Applies pending migrations
   - Seeds roles if none exist
   - Seeds admin/normal users if none exist
   - Updates role and user permissions
   - Seeds Mantine dashboard data

### Authentication & Authorization

**JWT Authentication**:
- Configured in Program.cs:87-136
- Token validation includes blacklist checking via `ITokenBlacklistRepository`
- Settings in `appsettings.json` under `JwtSettings`

**Permission-Based Authorization**:
- Custom attribute: `[PermissionAuthorize("PermissionName")]`
- Dynamic policy provider creates policies on-the-fly based on permission claims
- Claims checked via `CustomClaimTypes.Permission` constant
- Permissions stored as user claims and validated per request
- Custom `CustomAuthorizationMiddlewareResultHandler` provides user-friendly permission error messages

**Key Classes**:
- `PermissionAuthorizationPolicyProvider`: Dynamically creates authorization policies
- `PermissionAuthorizeAttribute`: Apply to controllers/actions for permission checks
- `PermissionMessageService`: Maps permissions to user-friendly error messages
- `IUserClaimsService`: Manages user permission claims

### Database Context

`ApplicationDbContext` (Data/ApplicationDbContext.cs) extends `IdentityDbContext<ApplicationUser>` and includes:
- Identity tables (Users, Roles, Claims)
- Core entities: `BlacklistedTokens`, `AuditLogs`
- Mantine dashboard entities: `DashboardStats`, `Sales`, `Orders`, `Projects`, `Invoices`, `KanbanTasks`, `UserProfiles`, `Files`, `Folders`, `FileActivities`, `Chats`, `ChatMessages`, `Languages`, `Countries`, `Traffic`

### Controllers Organization

**Base Controllers**:
- `BaseApiController`: Common base for all controllers, provides standardized API responses
- `MantineBaseController`: Base for Mantine dashboard controllers
- `AntdBaseController`: Base for Ant Design dashboard controllers

**Common Controllers**:
- `AuthController`: Login, register, logout, refresh token, password reset
- `UsersController`: User management (CRUD operations)
- `ProfileController`: Current user profile management
- `AuditController`: Audit log retrieval
- `LanguagesController`: System languages
- `CountriesController`: System countries

**Mantine Controllers**: Orders, Projects, Sales, Stats, Invoices, KanbanTasks, UserProfile, FileManagement, Communication, SystemConfig

### Service Layer Pattern

All business logic resides in services (Services/ directory) implementing interfaces (Interfaces/ directory). Controllers inject and delegate to services. This keeps controllers thin and business logic testable.

Example:
- Interface: `Interfaces/IUserService.cs`
- Implementation: `Services/UserService.cs`
- Registration: Program.cs:152-166

### Audit Logging

The `IAuditService` tracks entity changes:
- Logs user actions (Create, Update, Delete)
- Stores entity name, ID, action type, timestamp, and user
- Foreign key to `ApplicationUser` with SET NULL on delete

### Token Blacklist System

JWT tokens can be invalidated before expiration:
- `ITokenBlacklistRepository`: Manages blacklisted tokens
- `TokenCleanupService`: Background service that periodically removes expired tokens
- Token validation in JWT bearer events checks blacklist (Program.cs:109-128)

### Database Seeding

Seeders in `Data/Seeders/`:
- `RoleSeeder`: Creates Admin and User roles
- `AdminUserSeeder`/`NormalUserSeeder`: Creates default users
- `PermissionUpdateSeeder`: Updates role permissions
- `UserPermissionUpdateSeeder`: Updates user-specific permissions
- `MantineDataSeeder`: Seeds Mantine dashboard data

Seeding runs automatically on startup (Program.cs:188-242).

### CORS Configuration

CORS is configured in Program.cs:27-48. Allowed origins read from `appsettings.json` under `Cors:AllowedOrigins`. Defaults to allowing all origins if none configured (development fallback).

### API Documentation

API documentation available via Scalar UI at `/scalar/v1` when running the application.

## Key Conventions

**Commit Messages**: Follow conventional commit format (see git log for examples):
- `feat:` for new features
- `chore:` for maintenance tasks
- `fix:` for bug fixes

**DTOs**: Located in Dtos/ organized by feature area (Auth, UserManagement, Profile, Mantine, etc.)

**Null Handling**: Project has `<Nullable>disable</Nullable>` - nullability annotations not enforced

**Database**: PostgreSQL (via Npgsql.EntityFrameworkCore.PostgreSQL) - not SQL Server despite README mention

**Permissions**: Use `[PermissionAuthorize("Resource.Action")]` for fine-grained access control instead of role-based `[Authorize(Roles = "...")]`

## Adding New Features

1. **Entity**: Create in Entities/ (and Entities/Mantine/ for dashboard-specific)
2. **DbSet**: Add to ApplicationDbContext.cs and configure in OnModelCreating if needed
3. **Migration**: Run `dotnet ef migrations add <Name>`
4. **DTO**: Create request/response DTOs in Dtos/
5. **Interface**: Define service interface in Interfaces/
6. **Service**: Implement business logic in Services/
7. **Controller**: Create controller inheriting appropriate base, inject service
8. **Registration**: Register service in Program.cs dependency injection
9. **Permissions**: Define permission constants and add to seeders if using permission-based auth

## Running the Application

1. Ensure PostgreSQL is running
2. Update connection string in `appsettings.json`
3. Run `dotnet watch run`
4. Application will auto-migrate database and seed initial data
5. Access API docs at `/scalar/v1`
6. Default admin user credentials are in `Data/Seeders/AdminUserSeeder.cs`
