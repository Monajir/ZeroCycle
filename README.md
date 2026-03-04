# SmartWaste.Web ♻️🗺️

ASP.NET Core MVC web application for **Smart Waste Management** workflows — featuring **public map views** plus **role-based areas** for **Admin**, **Operator**, and **Driver**.

---

## ✨ What it does

- 🗺️ **Public Map**: browse locations/bins and service information
- 🧑‍💼 **Admin Area**: manage users/roles, zones, system configuration
- 🏭 **Operator Area**: assign tasks, monitor operations, validate statuses
- 🚚 **Driver Area**: follow routes/tasks, update collection progress
- 🔐 **Identity Area**: authentication + role-based authorization
- 🧩 Built with **ASP.NET Core MVC** + **EF Core** + **SQL Server** (or configured DB)

---

## ✅ Prerequisites

- **.NET SDK 9.0+**
- **SQL Server** (or your configured database)

---

## 🚀 Run locally

```bash
dotnet restore
dotnet run
```

The app uses URLs from: `Properties/launchSettings.json`

---

## 🏗️ Build

```bash
dotnet build
```

---

## 🗃️ Database (EF Core)

If migrations exist, apply them:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

Or run scripts from: `database_scripts/`

---

## ⚙️ Configuration

Configuration lives in:

- `appsettings.json`
- `appsettings.Development.json`

### Example connection string

Update `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SmartWaste;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

## 🧭 Project structure

| Path | What it contains |
|------|------------------|
| `Areas/` | Role-specific MVC areas (`Admin`, `Operator`, `Driver`, `Identity`) |
| `Controllers/` | Main app controllers |
| `Data/` | EF Core `ApplicationDbContext`, migrations |
| `Models/` | View models, DTOs |
| `Views/` | Razor views + shared layouts |
| `wwwroot/` | Static assets (CSS, JS, libraries) |
| `database_scripts/` | SQL schema, seeds, procedures, helpers |

---

## 🔐 Roles

- **Admin** — full control and configuration  
- **Operator** — operations monitoring and job assignment  
- **Driver** — field execution and status updates  

---

## 🧯 Troubleshooting

**Port already in use**
- Change it in `Properties/launchSettings.json`

**SQL connection errors**
- Check SQL Server is running
- Verify connection string + permissions
- Keep `TrustServerCertificate=True` for local dev if needed

**`dotnet ef` not found**
```bash
dotnet tool install --global dotnet-ef
```

---

## 📄 License

Add your license here (MIT / Apache-2.0 / Proprietary).  
Example: `MIT — see LICENSE`.
