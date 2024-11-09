# Admin Hub API

A centralized backend service powering multiple administrative dashboards. Built with ASP.NET Web API and MS SQL Server.

## Overview

Admin Hub API serves as the core backend infrastructure for managing multiple administrative dashboards. It provides a unified interface for data management, authentication, and business logic operations across different administrative frontend applications.

## Tech Stack

- **Framework**: ASP.NET Web API
- **Database**: Microsoft SQL Server
- **Authentication**: JWT-based authentication
- **Documentation**: Swagger/OpenAPI

## Features

- 🔐 Centralized authentication and authorization
- 🔄 RESTful API endpoints for multiple dashboard operations
- 📊 Data aggregation and reporting services
- 🔍 Advanced search and filtering capabilities
- 📝 Audit logging and activity tracking
- 🛡️ Role-based access control (RBAC)

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- MS SQL Server 2019 or later
- Visual Studio 2022 (recommended) or VS Code or Intellij Rider

### Installation

1. Clone the repository
```bash
git clone https://github.com/your-username/admin-hub-api.git
cd admin-hub-api
```

2. Update the connection string in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=AdminHubDB;Trusted_Connection=True;"
  }
}
```

3. Run database migrations
```bash
dotnet ef database update
```

4. Start the application
```bash
dotnet run
```

## Project Structure

```
admin-hub-api/
├── src/
│   ├── AdminHub.API/          # API project
│   ├── AdminHub.Core/         # Business logic and domain models
│   ├── AdminHub.Data/         # Data access layer
│   └── AdminHub.Common/       # Shared utilities and helpers
├── tests/
│   ├── AdminHub.API.Tests/
│   ├── AdminHub.Core.Tests/
│   └── AdminHub.Data.Tests/
└── docs/                      # Additional documentation
```

## API Documentation

API documentation is available via Swagger UI at `/swagger` when running the application in development mode.

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Support

For support and queries, please open an issue in the GitHub repository.