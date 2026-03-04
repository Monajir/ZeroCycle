# SmartWaste.Web

ASP.NET Core MVC web application for Smart Waste management workflows, including public map views and role-based areas for Admin, Operator, and Driver.

## Prerequisites

- .NET SDK 9.0+
- SQL Server (or your configured database)

## Run locally

```bash
dotnet restore
dotnet run
```

The app will start using the URLs configured in `Properties/launchSettings.json`.

## Build

```bash
dotnet build
```

## Project structure (high level)

- `Areas/` — Role-specific MVC areas (`Admin`, `Operator`, `Driver`, `Identity`)
- `Controllers/` — Main application controllers
- `Data/` — EF Core `ApplicationDbContext` and migrations
- `Models/` — View models and DTOs
- `Views/` — Razor views and shared layouts
- `wwwroot/` — Static assets (CSS, JS, libraries)
- `database_scripts/` — SQL scripts for schema, seeds, procedures, and helpers

## Configuration

- `appsettings.json`
- `appsettings.Development.json`

Update connection strings and environment-specific settings before running in your environment.
