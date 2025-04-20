# AdminHub API

A robust backend application built using .NET 9, ASP.NET Web APIs, and PostgreSQL for user management, authentication, and authorization.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Authentication & Authorization](#authentication--authorization)
- [API Endpoints](#api-endpoints)
- [User Management](#user-management)
- [Identity & Claims](#identity--claims)
- [Security](#security)
- [Areas for Improvement](#areas-for-improvement)

## Overview

AdminHub API is a backend service built with the latest .NET 9 framework that provides essential user management functionality including authentication, authorization with role and claim-based permissions, and user administration features.

## Features

- JWT-based authentication
- Role and claim-based authorization
- User management (CRUD operations)
- Password management (reset, forgot, change)
- Token refresh mechanism
- Admin seeding for initial setup
- Comprehensive claim and permission system

## Project Structure

### Core Components

- **Controllers**: Handle HTTP requests and responses
- **DTOs**: Data transfer objects for API request/response models
- **Entities**: Domain models for database persistence
- **Services**: Business logic implementation
- **Repositories**: Data access layer
- **Interfaces**: Abstraction for components
- **Constants**: Application-wide constant values

## Getting Started

### Prerequisites

- .NET 9 SDK
- PostgreSQL database
- JWT configuration in appsettings.json

### Configuration

Ensure your `appsettings.json` includes the following JWT settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=adminhub;Username=postgres;Password=yourpassword"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-should-be-at-least-32-chars",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "ExpirationInMinutes": 60
  }
}
```

### Running the Application

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run database migrations: `dotnet ef database update`
4. Start the application: `dotnet run`

The application will automatically:
- Apply any pending migrations
- Seed an admin user with all permissions

## Authentication & Authorization

### Authentication

The application uses JWT (JSON Web Token) authentication. Tokens are issued upon successful login and contain claims about the user, including:
- User identity (ID, username, email)
- Roles
- Custom claims (permissions, department, subscription level)

### Authorization

The application supports multiple authorization strategies:

#### Role-Based Authorization

Users can be assigned one or more roles (e.g., "Admin", "User"). Endpoints can be protected using the `[Authorize(Roles = "Admin")]` attribute.

#### Permission-Based Authorization

Fine-grained access control is implemented using custom claims:
- Permission claims (e.g., "ViewUsers", "EditUsers")
- Policy-based enforcement (e.g., "CanManageUsers" policy)

#### Subscription-Level Authorization

Access to premium features can be controlled based on the user's subscription level.

## API Endpoints

### Authentication

- **POST /api/auth/login**: Authenticate user and obtain JWT token
- **POST /api/auth/register**: Register a new user account
- **POST /api/auth/forgot-password**: Request password reset
- **POST /api/auth/reset-password**: Reset password using token
- **POST /api/auth/refresh-token**: Refresh an expired JWT token
- **POST /api/auth/logout**: User logout

## User Management

The UserService provides comprehensive user management functionality:

- **Get User**: Retrieve user details by ID
- **List Users**: Get paginated list of users with optional search
- **Create User**: Register new users with roles and claims
- **Update User**: Modify user details, roles, and claims
- **Delete User**: Remove user accounts
- **Change Password**: Allow users to change their password
- **Reset Password**: Administrative password reset
- **Manage Roles**: Update user role assignments

## Identity & Claims

### User Claims Service

The application implements a flexible claim management system that allows:

- Assignment of permission claims
- Department assignment
- Subscription level management
- Custom claim management

### Identity Seeding

The application seeds an initial admin user with:
- Username: admin
- Email: admin@example.com
- Password: Admin123! (change in production!)
- Role: Admin
- All available permissions
- Enterprise subscription level
- Management department

## Security

The application implements several security best practices:

- Secure password policies
- Account lockout after failed attempts
- Password hashing via ASP.NET Identity
- JWT token validation
- HTTPS enforcement
- Unique email requirement
- Token expiration

## Areas for Improvement

Based on the code review, consider the following improvements:

1. Complete the implementation of the logout endpoint in AuthController
2. Add user profile management functionality
3. Implement email confirmation workflow
4. Add multi-factor authentication support
5. Implement audit logging for security events
6. Consider adding refresh token table for better token management
7. Add rate limiting for authentication endpoints
8. Implement API versioning
9. Add comprehensive exception handling middleware
10. Create integration tests for authentication flows

---