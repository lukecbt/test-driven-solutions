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
        var group = app.MapGroup("/parking");

        group.MapPost("/", AddParking);
        group.MapGet("/", GetParking);
        group.MapPost("/exit", RemoveParking);
  }

    /// <summary>
    /// POST /parking: Parks a given vehicle in the first available space and returns the vehicle and its space number
    /// </summary>
    static async Task<Results<Ok<VehicleParkingResponse>, BadRequest<string>>> AddParking(IParkingService service, VehicleParkingRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleReg))
        {
            return TypedResults.BadRequest("Vehicle reg is required.");
        }

        if (request.VehicleType != 1 && request.VehicleType != 2 && request.VehicleType != 3)
        {
            return TypedResults.BadRequest("Invalid vehicle type.");
        }

        VehicleParkingResponse? response = await service.AddParking(request, ct);

        if (response == null)
        {
            return TypedResults.BadRequest("Cannot allocate vehicle, no spaces or vehicle already parked.");
        }

        return TypedResults.Ok(response);
    }

    /// <summary>
    /// GET /parking: Gets available and occupied number of spaces
    /// </summary>
    static async Task<Ok<ParkingResponse>> GetParking(IParkingService service, CancellationToken ct)
    {
        return TypedResults.Ok(await service.GetAvailableAndOccupiedSpaces(ct));
    }

    /// <summary>
    /// POST /parking/exit: Should free up this vehicles space and return its final charge from its parking time until now
    /// </summary>
    static async Task<Results<Ok<VehicleExitResponse>, BadRequest<string>, NotFound<string>>> RemoveParking(IParkingService service, VehicleExitRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.VehicleReg))
        {
            return TypedResults.BadRequest("Vehicle reg is required.");
        }

        VehicleExitResponse? response = await service.RemoveParking(request, ct);

        if (response is null)
        {
            return TypedResults.NotFound("Vehicle is not parked.");
        }

        return TypedResults.Ok(response);
    }
}
