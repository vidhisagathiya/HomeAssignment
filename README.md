# TravelExperience Assignment

This project implements a simple API to create travel experiences using ASP.NET Core and Entity Framework Core with Microsoft SQL Server running in Docker. 

---

## Database Choice & Rationale

**Microsoft SQL Server** was selected (running in Docker) because:

- Fully supported by Entity Framework Core (ideal for code-first approach).
- Easy cross-platform setup using Docker.
- Great fit for relational data like trips and activities.
- Compatible with Azure Data Studio for quick DB inspection.
Using Docker for MS SQL allowed for rapid development without installing the server directly on macOS.

---

## Key Design Decisions

- **Separation of Concerns**  
  Keeping controllers, services, data models, and DTOs separate ensures that each part of the application handles only what it's meant to. This improves readability and maintainability.

- **Controller Layer**  - Handles API endpoints (`/api/trips`)
  - Following best practices, controllers only handle HTTP concerns and delegate logic to the service layer for clarity and simplicity.

- **Service Layer (`TripService`)** - Calculates total cost of activities
  - By isolating business logic in the `TripService`, unit testing becomes straightforward without needing to run the full API.

- **Models & DTOs**  
  - Domain models represent database tables.
  - Using DTOs prevents over-posting and leaking internal database structures through the API.

- **Mapper**  
  - Centralized place for converting between Trip/Activity models and DTOs
  - Allows full control over how data is transformed, which is useful in small projects.

- **EF Core (Data Layer)**  
  - Enables rapid development and database schema evolution directly from the C# codebase, without manual SQL.
---

## How to Run the Code Locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Azure Data Studio (optional)](https://learn.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio)

---

### 1. Start MS SQL Server using Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Pass123" \
   -p 1433:1433 --name sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

### 2. Update EF Core Connection

Connection string is set in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=TravelExperienceDb;User Id=sa;Password=YourStrong@Pass123;TrustServerCertificate=True"
}
```

### 3. Apply EF Core Migrations

After configuring your connection to MS SQL Server, apply the EF Core migrations to create the database schema.

Run the following command from your project root (where the `.csproj` file is located):

```bash
dotnet ef database update
```
### 4. Run the API

```bash
cd TravelExperience
dotnet run
```
### 5. Test the API

Once running, the API should be available at:

```
http://localhost:5000
```

You can test endpoints using [Postman](https://www.postman.com/).


#### Sample Endpoint
- `POST /api/trips` – Create a new trip (with activities)

Example POST request body:

```json
{
  "userId": "user123",
  "title": "Trip 1",
  "startDate": "2025-06-01",
  "endDate": "2025-06-15",
  "activities": [
    {
      "destinationId": 101,
      "duration": 3,
      "cost": 40.00
    },
    {
      "destinationId": 102,
      "duration": 2,
      "cost": 50.00
    },
    {
      "destinationId": 103,
      "duration": 4,
      "cost": 10.00
    }
  ]
}

```

Response:

```json
{
    "experienceId": "1",
    "title": "Trip 1",
    "userId": "user123",
    "startDate": "2025-06-01T00:00:00",
    "endDate": "2025-06-15T00:00:00",
    "totalCost": 100,
    "activities": [
        {
            "destinationId": 101,
            "duration": 3,
            "cost": 40
        },
        {
            "destinationId": 102,
            "duration": 2,
            "cost": 50
        },
        {
            "destinationId": 103,
            "duration": 4,
            "cost": 10
        }
    ]
}

```
>  `totalCost` is automatically calculated based on all activity costs.
---
## Project Structure

```
TravelExperience/
│
├── Controllers/
│   └── TripsController.cs          # Exposes API endpoints
│
├── Services/
│   ├── ITripService.cs            # Service interface
│   └── TripService.cs             # Business logic
│
├── Models/
│   ├── Trip.cs                    # Trip entity
│   └── Activity.cs                # Activity entity
│
├── DTOs/
│   ├── TripCreateDto.cs          # For incoming POST requests
│   ├── TripReadDto.cs            # For outgoing responses
│   └── ActivityDto.cs
│
├── Mappers/
│   └── TripMapper.cs             # Manual DTO ↔ Entity mapping
│
├── Data/
│   └── AppDbContext.cs           # EF Core DbContext
│
└── appsettings.json              # Configuration & connection string
```

---
