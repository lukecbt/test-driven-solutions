# Test Driven Solutions - Parking API

## Running the project

### Through Visual Studio

Run and the app should be served at https://localhost:7280

### With Docker

Build the docker file from /TestDrivenSolutions.Api

```
docker build -t test-driven-solutions -f Dockerfile .
```

Then run the image from /TestDrivenSolutions.Api (port 8080)

```
docker run -p 8080:8080 test-driven-solutions
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
Host=localhost;Port=5432;Database=parkingdb;Username=postgres;Password=
```

### Running Tests

Integration tests will be run against a test database that would be required alongside the local database.

```ConnectionStrings:TestConnection```

Unit tests should run without any dependencies.
