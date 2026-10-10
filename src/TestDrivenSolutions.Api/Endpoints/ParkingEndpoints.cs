using Microsoft.AspNetCore.Http.HttpResults;
using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;

namespace TestDrivenSolutions.Api.Endpoints;

public static class ParkingEndpoints
{
  /// <summary>
  /// Extend the application and create the parking endpoints seperate from Program.cs
  /// </summary>
  /// <param name="app"></param>
  public static void RegisterParkingEndpoints(this WebApplication app)
  {
        // TODO: exception handler?

        var group = app.MapGroup("/parking");

        group.MapPost("/", AddParking);
        group.MapGet("/", GetParking);
        group.MapPost("/exit", RemoveParking);
  }

    /// <summary>
    /// POST /parking: Parks a given vehicle in the first available space and returns the vehicle and its space number
    /// </summary>
    static async Task<Results<Ok<VehicleParkingResponse>, BadRequest<string>, NotFound<string>>> AddParking(IParkingService service, VehicleParkingRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleReg))
        {
            return TypedResults.BadRequest("Vehicle reg is required.");
        }

        VehicleParkingResponse? response = await service.AddParking(request);

        if (response == null)
        {
            return TypedResults.NotFound("No available parking spaces.");
        }

        return TypedResults.Ok(response);
    }

    /// <summary>
    /// GET /parking: Gets available and occupied number of spaces
    /// </summary>
    static async Task<Ok<ParkingResponse>> GetParking(IParkingService service)
    {
        return TypedResults.Ok(await service.GetAvailableAndOccupiedSpaces());
    }

    /// <summary>
    /// POST /parking/exit: Should free up this vehicles space and return its final charge from its parking time until now
    /// </summary>
    static async Task<Results<Ok<VehicleExitResponse>, BadRequest<string>>> RemoveParking(IParkingService service, VehicleExitRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleReg))
        {
            return TypedResults.BadRequest("Vehicle reg is required.");
        }
        return TypedResults.Ok(await service.RemoveParking(request));
    }
}
