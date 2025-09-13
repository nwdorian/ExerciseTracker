# Exercise Tracker

This application tracks exercise data. It allows users to save exercise details like date, duration and description as well as assign categories.

Backend is a .NET Web API with REST endpoints for managing exercise and category records.

Frontend is a .NET console application.

## Table of contents

- [Exercise Tracker](#exercise-tracker)
  - [Table of contents](#table-of-contents)
  - [Technologies](#technologies)
  - [Getting started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
  - [Solution structure](#solution-structure)
    - [Clean Architecture](#clean-architecture)
    - [WebApi Best Practices](#webapi-best-practices)
  - [Contributing](#contributing)
  - [License](#license)
  - [Contact](#contact)

## Technologies

- **.NET 9**
- **SQL Server**
- **Entity Framework Core**

## Getting started

### Prerequisites

- .NET 9 SDK
- SQL Server (Developer, Express, Docker container)
- An IDE like VS Code or VS2022
- Database management tool like SSMS or Dbeaver (optional)

### Installation

> [!NOTE]
> The `InitialCreate` migration was created.
>
> It will be applied on startup of the API application and create the database and tables.

1. Clone the repository
    - `git clone https://github.com/nwdorian/ExerciseTracker.git`

2. Configure `appsettings.json`
    - replace the connection string with your own if necessary

3. Navigate to the API directory and run the project
    - `cd src\ExerciseTracker.WebApi`
    - `dotnet run`

4. Navigate to the Console directory and run the project
    - `cd src\ExerciseTracker.Console`
    - `dotnet run`

## Solution structure

- Consistent code style enforced by `.editorconfig`
- Build configuration through `Directory.Build.props`
  - configuring .NET 9 as target framework for all projects
  - code quality properties
- Central package management through `Directory.Packages.Props`
- Static code analysis
  - Treat warnings as errors
  - Analysis level set to `latest-Recommended`
  - `SonarAnalyzer.CSharp` package

### Clean Architecture

Domain layer

- Domain models
- Doesn't have any external dependencies

Application layer

- Business logic classes (services)
- Abstractions implemented in Infrastructure
- Custom errors
- Application DTOs

Infrastructure layer

- EF Core DbContext configuration
- EF Core Migrations
- Repositories
- Seeding configuration

Presentation layer

- WebApi project
  - composition root of the solution
  - REST endpoints
- Console project
  - user interface for consuming the WebApi
- Contracts project
  - presentation layer DTOs - REST Models
  - shared between WebApi and Console projects

### WebApi Best Practices

- Api versioning
- Input validation with FluentValidation
- Result pattern
- Structured logging with Serilog
- Soft delete using EF Core interceptors
- Audit fields using EF Core interceptors
- EF Core configuration classes
- Global exception handling
- ProblemDetails [RFC7808](https://datatracker.ietf.org/doc/html/rfc7807) standard

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue.
