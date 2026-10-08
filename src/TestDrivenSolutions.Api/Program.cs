using TestDrivenSolutions.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// TODO: Entity framework with postgres?

// Health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Set simple health check
app.MapHealthChecks("/health");

app.UseHttpsRedirection();

// Register endpoints
app.RegisterParkingEndpoints();

app.Run();
