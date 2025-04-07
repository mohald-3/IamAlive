# IamAlive API

**IamAlive** is an ASP.NET Core Web API project designed to help users stay connected through location check-ins, emergency contact info, social groups, and friend connections. It supports secure authentication using JWT and follows a clean architecture with DTOs, services, and validation.

---

## Features

- ✅ JWT Authentication (Login & Registration)
- ✅ User CRUD operations with soft delete and patch support
- ✅ Check-In tracking (per user)
- ✅ Friendships (create, view, delete)
- ✅ Filtering, sorting, and pagination for user listings
- ✅ FluentValidation for input validation
- ✅ Fake data seeding with Bogus
- ✅ Swagger API documentation
- ✅ Modular, layered architecture

---

## Technologies

- ASP.NET Core 7 Web API
- Entity Framework Core + SQL Server
- AutoMapper
- FluentValidation
- Bogus (for fake data)
- Swagger / Swashbuckle
- JWT Authentication

---

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- SQL Server (local or remote)

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/IamAlive.git
   cd IamAlive
   ```

2. **Update your connection string** in `appsettings.json`

3. **Run migrations and seed database**
   ```bash
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run
   ```

---

## Usage

- Access Swagger UI at: `https://localhost:<port>/swagger`
- Use `POST /api/user/register` and `POST /api/user/login` to create and authenticate users.
- Use the **Authorize** button in Swagger to attach your JWT token.

---

## Folder Structure

```
IamAlive/
│
├── Controllers/         # API endpoints
├── Data/                # DbContext and seeder
├── DTOs/                # Data transfer objects
├── Helpers/             # Configuration helpers
├── Mapping/             # AutoMapper profiles
├── Models/              # Entity models
├── Services/            # Business logic
├── Validators/          # FluentValidation rules
└── Program.cs           # Application startup
```

---

## Future Improvements

- Refresh token flow
- Role-based authorization (e.g., Admin)
- Soft-deleted user filtering toggle
- Group Membership & Social Group expansion
- Notification system for check-ins

---

## License

MIT License — use freely and improve!
