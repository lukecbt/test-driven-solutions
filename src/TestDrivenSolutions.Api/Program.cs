using TestDrivenSolutions.Api.Endpoints;
using TestDrivenSolutions.Api.Handlers;
using TestDrivenSolutions.Application;
using TestDrivenSolutions.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Post gres setup from infrastructure project
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Add exception handling
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

// Health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

// Set simple health check
app.MapHealthChecks("/health");

app.UseHttpsRedirection();

// Register endpoints
app.RegisterParkingEndpoints();

app.Run();
