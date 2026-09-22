# LibrarySystem

A simple library management API built with .NET 10 and C# 14. This repository contains an ASP.NET Core Web API (LibrarySystem.Api) plus application and domain layers for managing books, members and basic library operations.

This README follows best practices and provides instructions for setup, running, testing, and contributing.

## Table of Contents
- Project Overview
- Tech Stack
- Prerequisites
- Getting Started
  - Clone
  - Build
  - Run (Visual Studio / CLI)
- Configuration
- API Endpoints (example)
- Running Tests
- Contributing
- Code Style & Guidelines
- Troubleshooting
- License

## Project Overview
LibrarySystem is organized to follow clean architecture principles. Typical layers include:
- LibrarySystem.Api — ASP.NET Core Web API project exposing HTTP endpoints.
- LibrarySystem.Application — application services, DTOs and use cases.
- LibrarySystem.Domain — domain models and business rules.
- LibrarySystem.Infrastructure (if present) — persistence, third-party integrations.

The API exposes endpoints for managing library members (create, update, deactivate, reactivate), and likely for books and borrowing operations.

## Tech Stack
- .NET 10
- C# 14
- ASP.NET Core Web API
- (Optional) Entity Framework Core or other persistence providers
- Visual Studio 2026 / dotnet CLI

## Prerequisites
- .NET 10 SDK installed: https://dotnet.microsoft.com/
- Visual Studio 2026 (recommended) or another editor/IDE that supports .NET 10
- Optional: SQL Server / PostgreSQL / other DB if the project uses a database. Check appsettings.json or repository docs.

## Getting Started
1. Clone the repository:

   git clone https://github.com/Yael353/LibrarySystem.git
   cd LibrarySystem

2. Restore and build (CLI):

   dotnet restore
   dotnet build

3. Run the solution

- From Visual Studio 2026
  - Open `LibrarySystem.slnx`, set `LibrarySystem.Api` as the startup project and run (F5/Ctrl+F5).

- From the command line (dotnet CLI):

   dotnet run --project LibrarySystem.Api

The API should start and listen on the configured port (see console output or appsettings). If launchSettings.json is present, Visual Studio and dotnet run will honor those settings.

## Configuration
Configuration is typically in `appsettings.json` under the `LibrarySystem.Api` project. Common settings:
- Connection strings for the database
- Logging settings
- API and feature flags

If the app requires environment variables, set them in your shell or via Visual Studio launch profile.

## API Endpoints (Members example)
This repository includes `MembersController` exposing REST endpoints. Example routes and usage:

Base path: /api/members

- GET /api/members
  - Returns list of members
  - Example: curl http://localhost:5000/api/members

- GET /api/members/{id}
  - Returns single member or 404
  - Example: curl http://localhost:5000/api/members/00000000-0000-0000-0000-000000000000

- POST /api/members
  - Creates a member. Body (JSON):
	{
	  "firstName": "Alice",
	  "lastName": "Johnson",
	  "email": "alice@example.com",
	  "phoneNumber": "123-456-7890"
	}
  - Example (curl):
	curl -X POST http://localhost:5000/api/members -H "Content-Type: application/json" -d '{"firstName":"Alice","lastName":"Johnson","email":"alice@example.com","phoneNumber":"123-456-7890"}'

- PUT /api/members/{id}
  - Updates a member. Body same shape as POST.

- DELETE /api/members/{id}
  - Deactivates a member.

- POST /api/members/{id}/activate
  - Reactivates a member.

Adjust host/port according to your environment.

## OpenAPI / Swagger and Postman

If the API project includes Swagger (Swashbuckle) the API documentation and an interactive UI will be available when the app is running.

- Run the API (Visual Studio or dotnet run). By default Swagger UI is usually available at: http://localhost:<port>/swagger
- Use the interactive Swagger UI to explore endpoints, see request/response schemas and try requests.

Exporting a Postman collection (example):

1. From Swagger UI click the "Export" or "Download" button if available, or copy the OpenAPI (JSON) URL (for example: http://localhost:<port>/swagger/v1/swagger.json).
2. In Postman: File > Import > Link and paste the swagger JSON URL, or import the downloaded JSON file. Postman will convert the OpenAPI spec into a collection.

Example quick curl request (replace host/port):

curl -X GET "http://localhost:5073/api/members" -H "accept: application/json"

## Local database setup & EF Core migrations

If the project uses Entity Framework Core for persistence, follow these steps to configure a local database and run migrations. The examples below use SQL Server and SQLite — adjust to your provider as needed.

1) Install the EF Core CLI tool (if not already):

   dotnet tool install --global dotnet-ef

2) Ensure the Api or Infrastructure project references the EF Core packages and the Design package for migrations. Example packages (add to the project that contains the DbContext):

   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.SqlServer    (or Microsoft.EntityFrameworkCore.Sqlite)
   - Microsoft.EntityFrameworkCore.Design

3) Configure the connection string in `LibrarySystem.Api/appsettings.Development.json` or `appsettings.json` under the Api project. Example (SQL Server):

   "ConnectionStrings": {
	 "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibrarySystemDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }

4) Create an initial migration and apply it. Run these commands from the solution root or from the project folder — specify the project that contains the DbContext and the startup project if different:

   dotnet ef migrations add InitialCreate --project LibrarySystem.Infrastructure --startup-project LibrarySystem.Api
   dotnet ef database update --project LibrarySystem.Infrastructure --startup-project LibrarySystem.Api

Notes:
- If your DbContext is in the Api project, omit the --project parameter or point both to the Api project.
- From Visual Studio Package Manager Console you can run Add-Migration and Update-Database commands with the Default Project set to the project that contains the DbContext.
- For SQLite, change the provider package to `Microsoft.EntityFrameworkCore.Sqlite` and update the connection string accordingly (e.g., Data Source=library.db).

Troubleshooting:
- If dotnet ef cannot find the DbContext, ensure the project builds and the DbContext is public and has a constructor that accepts DbContextOptions<T>.
- Ensure Microsoft.EntityFrameworkCore.Design package is referenced in the project containing the DbContext.

## Running Tests
If the repository contains test projects, run tests using the dotnet CLI or Visual Studio Test Explorer.

- From CLI:

  dotnet test

- From Visual Studio: Test > Run All Tests

Filter or run individual test projects as needed.

## Contributing
- Fork the repository, create a feature branch, implement changes and open a pull request.
- Follow existing coding conventions and write unit tests for new behavior.
- Keep changes small and focused.

## Code Style & Guidelines
- Follow idiomatic C# and project conventions already present in the repo.
- Prefer async/await for I/O operations and CancellationToken support on public async APIs.
- Keep controllers thin; delegate business logic to application services.

## Troubleshooting
- If migrations or DB connection issues occur, check connection strings and ensure the database is running.
- If ports conflict, update `launchSettings.json` or the `ASPNETCORE_URLS` environment variable.

## License
If this project does not yet contain a license file, add one appropriate to your needs (for example, MIT). If a LICENSE file is present in the repo, follow its terms.
