# API_Devenir_Dev_2 — Solution Overview

Version: .NET 8 | C# 12

This repository implements a layered ASP.NET Core Web API with JWT authentication and Entity Framework Core for persistence. This README explains the solution structure, security, EF Core usage, configuration, running and maintenance instructions — written in a teacher-style format so you can use it for learning or as project documentation.

Table of contents
- Project summary
- Solution structure
- Architecture & design principles
- Security (JWT) and recommended practices
- Entity Framework Core (DbContext, migrations, seeding, patterns)
- Dependency injection
- Configuration (appsettings keys & sample)
- Running locally
- Database migrations & seeding
- Endpoints & DTO notes
- Example HTTP requests 
- Troubleshooting & common errors
- Suggested improvements and roadmap

Project summary

- API project: `API_Devenir_Dev_2` — ASP.NET Core Web API; composition root in `Program.cs`.
- Application layer: `Application_Devenir_Dev_2` — services, DTOs, helpers.
- Domain layer: `Domain_Devenir_Dev_2` — interfaces and domain concepts.
- Infrastructure: `Infrastructure.Devenir_Dev_2` — EF Core `MovieContext`, repositories.

Key features
- JWT-based authentication with `Microsoft.AspNetCore.Authentication.JwtBearer`.
- Entity Framework Core with SQL Server.
- CORS policy `localdev` (permissive for development).
- Swagger enabled in development.

Solution structure and responsibilities

- `API_Devenir_Dev_2`
  - `Program.cs` — DI registration, middleware pipeline, CORS, JWT.
  - `Controllers` — HTTP endpoints.
  - `appsettings.json` / environment files — configuration.
- `Application_Devenir_Dev_2`
  - `Services` — application/business logic.
  - `DTOS` — request/response shapes.
  - `Tools` — helpers such as JWT helpers.
- `Domain_Devenir_Dev_2`
  - `Interfaces` — repository/service interfaces.
- `Infrastructure.Devenir_Dev_2`
  - `Data` — `MovieContext` (DbContext).
  - `Repositories` — concrete data access.

Architecture & design principles (teacher notes)

- Layered architecture: API -> Application -> Domain -> Infrastructure.
- Single Responsibility: controllers handle HTTP, services handle use-cases, repositories handle persistence.
- Dependency Inversion: controllers and services depend on interfaces.
- Testability: keep business logic in `Application` so you can mock repositories in unit tests.

Security — JWT and best practices

This project uses symmetric-key JWT signing for development.

Important configuration keys:
- `JWT:Issuer`
- `JWT:Audience`
- `JWT:secretKey` (development only; never store production secrets in repo)

Token validation options used in `Program.cs`:
- `ValidateIssuer = true`
- `ValidateAudience = true`
- `ValidateLifetime = true`
- `ValidateIssuerSigningKey = true`

Best practices
- Never commit production secrets. Use environment variables or a secret store (Azure Key Vault, HashiCorp Vault).
- Use a long, random key (at least 256-bit) for symmetric signing. Prefer asymmetric signing (RSA) in production for key rotation.
- Implement refresh tokens for longer sessions and revocation support.
- Use role/policy-based authorization as needed.
- Enable HTTPS and HSTS in production.

Entity Framework Core

DbContext registration:
- `MovieContext` is registered with `AddDbContext<MovieContext>(options => options.UseSqlServer(...))`.
- Development connection string key: `myConnection` (used when DEBUG compilation symbol is present).
- Production connection string key: `Prod` (used otherwise).

Migrations
- Keep migrations inside the infrastructure project that owns the DbContext (recommended: `Infrastructure.Devenir_Dev_2`).
- Example commands (from solution root):
  - Add migration: `dotnet ef migrations add InitialCreate --project Infrastructure.Devenir_Dev_2 --startup-project API_Devenir_Dev_2`
  - Update database: `dotnet ef database update --project Infrastructure.Devenir_Dev_2 --startup-project API_Devenir_Dev_2`

Seeding and patterns
- Use `OnModelCreating` or a dedicated seeder class and call it at startup (only in dev or when safe).
- Use `AsNoTracking()` for read-only queries.
- Paginate large queries and add indexes for frequently queried columns.

Dependency injection

Registrations in the project:
- `IMovieService -> MovieService` (scoped)
- `IMovieRepository -> MovieRepository` (scoped)
- `MovieContext` (scoped via `AddDbContext`)

Keep DbContext as `Scoped`. Register thread-safe, stateless utilities as `Singleton` where appropriate.

Configuration

Application expects the following configuration keys in `appsettings` or environment variables:
- `ConnectionStrings:myConnection` (development)
- `ConnectionStrings:Prod` (production)
- `JWT:Issuer`
- `JWT:Audience`
- `JWT:secretKey`

Running locally

Prerequisites
- .NET 8 SDK
- SQL Server or LocalDB
- `dotnet-ef` tool (optional): `dotnet tool install --global dotnet-ef`

Quickstart
1. Clone repository.
2. Create `appsettings.Development.json` or set environment variables (example file provided in `API_Devenir_Dev_2/appsettings.Development.json`).
3. Run migrations (see EF commands above).
4. Start API: `dotnet run --project API_Devenir_Dev_2`.
5. Open Swagger UI (development only) to explore endpoints.

Endpoints & DTO notes

- `AccountController` — likely endpoints: `POST /api/account/register`, `POST /api/account/login` to obtain JWTs.
- `MovieController` — typical endpoints: `GET /api/movies`, `GET /api/movies/{id}`, `POST /api/movies`, `PUT /api/movies/{id}`, `DELETE /api/movies/{id}`.

Refer to controller code for exact DTO shapes. Use DTOs for request/response mapping rather than exposing EF entities directly.

Example HTTP requests

See `EXAMPLES_HTTP_REQUESTS.md` for curl and Postman examples including how to use the `Authorization: Bearer <token>` header.


Deployment & production checklist

- Move secrets to Key Vault / environment variables.
- Ensure HTTPS and HSTS.
- Lock down CORS to allowed origins.
- Use structured logging (Serilog) and monitoring (Application Insights).
- Add health checks and readiness probes.

Troubleshooting & common errors

- Token validation errors: check `JWT:secretKey` and other JWT settings across services.
- DB errors: confirm connection string and that migrations were applied.
- CORS issues: adjust policy for production as needed.

Suggested improvements

- Implement refresh tokens and revocation.
- Adopt asymmetric signing for JWT and key-rotation plan.
- Add validation (FluentValidation) for DTOs.
- Add unit and integration test projects.
- Add Docker and compose for local environment (SQL Server container).

