# Test Driven Solutions - Parking API

## Running the project

### Through Visual Studio

Run and the app should be served at https://localhost:7280

### With Docker

Build the docker file from root

```
docker build -t test-driven-solutions -f src/TestDrivenSolutions.Api/Dockerfile src
```

Then run the image from root (port 8080)

```
docker run -p 8080:8080 test-driven-solutions -f src/TestDrivenSolutions.Api/Dockerfile src
```

### Health Check

Check the health check at "/health"

## Database

Add database connection to app settings prior to migrations and database update.

```ConnectionStrings:DefaultConnection```

### Apply Migrations

Apply current migrations to the configured database via package manager console (select TestDrivenSolutions.Infrastructure as default project)

```
Update-Database
```

### Connection String

Example local connection string:

```
Host=localhost;Port=5432;Database=parkingdb;Username=postgres;Password=
```

Example test connection string:

```
Host=localhost;Port=5432;Database=parkingdb_test;Username=postgres;Password=
```

### Running Tests

Integration tests will be run against a test database that would be required alongside the local database.

```ConnectionStrings:TestConnection```

Unit tests should run without any dependencies.

---

## Developer Notes

### Project

This project follows clean architecture split into the following projects:

**TestDrivenSolutions.Api**: Entry point for the project. Serves the API endpoints following .NETs minimal API structure. And also handles exceptions and dependency injection using extensions from the downstream layers.

**TestDrivenSolutions.Application**: Handles the application logic, model mapping and acts as a pass through to database operations.

**TestDrivenSolutions.Infrastructure**: Handles the database operations independent of the rest of the project. And currently uses PostgreSQL with Entity Framework.

**TestDrivenSolutions.Domain**: Holds business rules such as database entities. And is independant from the rest of the project.

**TestDrivenSolutions.Tests**: Handles the integration and unit tests for the different project layers.

### Storage

The database contains the following tables:

**Spaces**: Stores information on the current space availability. Stores car information alongside this including time arrived.

**VehicleTypeTarrif**: Stores configuartions for the prices per vehicle sizes.

### Assumptions

- There was 20 spaces within the parking application
- That price configuations could change so needed to be amendable
- Error responses and request handling
- If vehicle type doesn't exist in databse (seed error or deletion), default to small vehicle charges
- If vehicle isn't assigned to a space on exit, reject
- No two vehicles of the same reg allowed

### Future improvements

- Better request handling with results and status to map to client responses
- Auditing exited vehicles
- Ensuring multiple requests can't allocate the same space
- Observability setup and more detailed logging/errors
- CI/CD workflow (build, test on merge)
- Docker compose dev setup with container database
