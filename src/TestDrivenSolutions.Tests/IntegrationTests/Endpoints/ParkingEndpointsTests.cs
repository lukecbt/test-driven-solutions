using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Models.Response;
using TestDrivenSolutions.Domain.Entities;
using TestDrivenSolutions.Infrastructure;

namespace TestDrivenSolutions.Tests.IntegrationTests.Endpoints;

public class ParkingEndpointsTests
{
    private CustomWebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _factory = new();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task AddParking_WhenSuccessful_ShouldReturnRegAndSpaceNumber()
    {
        // Arrange
        VehicleParkingRequest request = new()
        {
            VehicleReg = "TESTREG",
            VehicleType = 1
        };

        // Act
        var response = await _client.PostAsync("/parking", JsonContent.Create(request));

        // Assert
        response.EnsureSuccessStatusCode();
        VehicleParkingResponse result = JsonSerializer.Deserialize<VehicleParkingResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.VehicleReg.Should().Be("TESTREG");
        result.SpaceNumber.Should().Be(1);
    }

    [Test]
    public async Task AddParking_WhenNoVehicleReg_ShouldReturnBadRequest()
    {
        // Arrange
        VehicleParkingRequest request = new()
        {
            VehicleReg = "",
            VehicleType = 1
        };

        // Act
        var response = await _client.PostAsync("/parking", JsonContent.Create(request));

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task AddParking_WhenNoSpaces_ShouldReturnNotFound()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DateTime timeIn = DateTime.UtcNow;
        List<Space> dbSpaces = new();
        for (int i = 0; i < 20; i++)
        {
            dbSpaces.Add(new() { Id = i + 1, IsOccupied = true, CurrentVehicleReg = "TEST" + i, CurrentVehicleType = 1, TimeIn = timeIn });
        }
        context.UpdateRange(dbSpaces);
        await context.SaveChangesAsync();

        VehicleParkingRequest request = new()
        {
            VehicleReg = "TESTREG",
            VehicleType = 1
        };

        // Act
        var response = await _client.PostAsync("/parking", JsonContent.Create(request));

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetParking_WhenSuccessful_ShouldReturnAvailableAndOccupiedSpaces()
    {
        // Act
        var response = await _client.GetAsync("/parking");

        // Assert
        response.EnsureSuccessStatusCode();
        ParkingResponse? result = JsonSerializer.Deserialize<ParkingResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.AvailableSpaces.Should().Be(20);
        result.OccupiedSpaces.Should().Be(0);
    }

    [Test]
    public async Task GetParking_WhenOneParkingIsAdded_ShouldReturn1OccupiedSpace()
    {
        // Arrange
        VehicleParkingRequest request = new()
        {
            VehicleReg = "TESTREG",
            VehicleType = 1
        };

        // Act
        var addParkingResponse = await _client.PostAsync("/parking", JsonContent.Create(request));
        var getParkingResponse = await _client.GetAsync("/parking");

        // Assert
        addParkingResponse.EnsureSuccessStatusCode();
        getParkingResponse.EnsureSuccessStatusCode();
        ParkingResponse? result = JsonSerializer.Deserialize<ParkingResponse>(await getParkingResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.AvailableSpaces.Should().Be(19);
        result.OccupiedSpaces.Should().Be(1);
    }

    [Test]
    public async Task RemoveParking_WhenSmallCarStaysFor10Minutes_ShouldReturnChargeOf3Pound()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DateTime timeIn = DateTime.UtcNow.AddMinutes(-10);
        Space dbSpace = new()
        {
            Id = 1,
            CurrentVehicleReg = "TESTREG",
            CurrentVehicleType = 1,
            IsOccupied = true,
            TimeIn = timeIn
        };

        context.Update(dbSpace);
        await context.SaveChangesAsync();

        VehicleExitRequest request = new()
        {
            VehicleReg = "TESTREG"
        };

        // Act
        var response = await _client.PostAsync("/parking/exit", JsonContent.Create(request));

        // Assert
        response.EnsureSuccessStatusCode();
        VehicleExitResponse? result = JsonSerializer.Deserialize<VehicleExitResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.VehicleReg.Should().Be("TESTREG");
        result.VehicleCharge.Should().Be(3);
    }

    [Test]
    public async Task RemoveParking_WhenMediumCarStaysFor10Minutes_ShouldReturnChargeOf4Pound()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DateTime timeIn = DateTime.UtcNow.AddMinutes(-10);
        Space dbSpace = new()
        {
            Id = 1,
            CurrentVehicleReg = "TESTREG",
            CurrentVehicleType = 2,
            IsOccupied = true,
            TimeIn = timeIn
        };

        context.Update(dbSpace);
        await context.SaveChangesAsync();

        VehicleExitRequest request = new()
        {
            VehicleReg = "TESTREG"
        };

        // Act
        var response = await _client.PostAsync("/parking/exit", JsonContent.Create(request));

        // Assert
        response.EnsureSuccessStatusCode();
        VehicleExitResponse? result = JsonSerializer.Deserialize<VehicleExitResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.VehicleReg.Should().Be("TESTREG");
        result.VehicleCharge.Should().Be(4);
    }

    [Test]
    public async Task RemoveParking_WhenLargeCarStaysFor10Minutes_ShouldReturnChargeOf6Pound()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DateTime timeIn = DateTime.UtcNow.AddMinutes(-10);
        Space dbSpace = new()
        {
            Id = 1,
            CurrentVehicleReg = "TESTREG",
            CurrentVehicleType = 3,
            IsOccupied = true,
            TimeIn = timeIn
        };

        context.Update(dbSpace);
        await context.SaveChangesAsync();

        VehicleExitRequest request = new()
        {
            VehicleReg = "TESTREG"
        };

        // Act
        var response = await _client.PostAsync("/parking/exit", JsonContent.Create(request));

        // Assert
        response.EnsureSuccessStatusCode();
        VehicleExitResponse? result = JsonSerializer.Deserialize<VehicleExitResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        result.Should().NotBeNull();
        result.VehicleReg.Should().Be("TESTREG");
        result.VehicleCharge.Should().Be(6);
    }

    [Test]
    public async Task RemoveParking_WhenCarIsNotParked_ShouldReturnNotFound()
    {
        // Arrange
        VehicleExitRequest request = new()
        {
            VehicleReg = "TESTREG"
        };

        // Act
        var response = await _client.PostAsync("/parking/exit", JsonContent.Create(request));

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}