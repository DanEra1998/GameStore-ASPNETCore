# GameStore API

A RESTful API built with ASP.NET Core and Entity Framework Core, using SQLite as the database. Built as a learning project to explore .NET backend development fundamentals.

## Tech Stack

- .NET 10
- ASP.NET Core (Minimal APIs)
- Entity Framework Core
- SQLite
- JetBrains Rider

## Project Structure

```
GameStore.Api/
├── Data/
│   ├── Migrations/         # EF Core migration files
│   ├── DataExtensions.cs   # Seeding and data helpers
│   └── GameStoreContext.cs # EF Core DbContext
├── DTOs/
│   ├── CreateGameDTO.cs    # Incoming POST payload
│   ├── UpdateGameDTO.cs    # Incoming PUT payload
│   └── GameDto.cs          # Outgoing response shape
├── Endpoints/
│   └── GamesEndpoints.cs   # All game-related API endpoints
├── Models/
│   ├── Game.cs             # Game entity
│   └── Genre.cs            # Genre entity
├── games.http              # HTTP request file for testing endpoints
└── Program.cs              # App entry point and configuration
```

## API Endpoints

| Method | Route         | Description            |
|--------|---------------|------------------------|
| GET    | /games        | Get all games          |
| GET    | /game/{id}    | Get a game by ID       |
| POST   | /games        | Create a new game      |
| PUT    | /games/{id}   | Update an existing game|
| DELETE | /games/{id}   | Delete a game          |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [JetBrains Rider](https://www.jetbrains.com/rider/) or any .NET compatible IDE

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/DanEra1998/GameStore-ASPNETCore.git
   cd GameStore-ASPNETCore
   ```

2. Navigate to the project folder
   ```bash
   cd GameStore.Api
   ```

3. Install dependencies
   ```bash
   dotnet restore
   ```

4. Apply database migrations
   ```bash
   dotnet ef database update
   ```

5. Run the project
   ```bash
   dotnet run
   ```

6. The API will be available at `http://localhost:5222`

### Testing Endpoints

Open `games.http` in Rider to run requests directly against the API using the built-in HTTP client.

## Notes

- The SQLite database file (`GameStore.db`) is excluded from version control via `.gitignore`
- Anyone cloning this repo can recreate the database locally by running `dotnet ef database update`
