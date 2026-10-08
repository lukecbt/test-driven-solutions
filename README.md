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
