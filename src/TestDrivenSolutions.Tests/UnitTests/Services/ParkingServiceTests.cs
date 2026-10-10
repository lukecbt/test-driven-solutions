using FluentAssertions;
using Moq;
using TestDrivenSolutions.Application.Interfaces;
using TestDrivenSolutions.Application.Models.Request;
using TestDrivenSolutions.Application.Services;
using TestDrivenSolutions.Domain.Entities;

namespace TestDrivenSolutions.Tests.UnitTests.Services;

public class ParkingServiceTests
{
    private Mock<IParkingRepository> _mockRepository;
    private IParkingService _service;
    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IParkingRepository>();
        _service = new ParkingService(_mockRepository.Object);
    }

    [Test]
    public async Task GetAvailableAndOccupiedSpaces_WhenSuccessful_ShouldMapAvailableAndOccupiedSpaces()
    {
        // Arrange
        int available = 20;
        int occupied = 0;
        _mockRepository.Setup(m => m.GetSpaceCountsAsync()).ReturnsAsync((available, occupied));

        // Act
        var result = await _service.GetAvailableAndOccupiedSpaces();

        // Assert
        result.Should().NotBeNull();
        result.AvailableSpaces.Should().Be(available);
        result.OccupiedSpaces.Should().Be(occupied);
    }

    [Test]
    public async Task AddParking_WhenSuccessful_ShouldMapRegAndSpaceNumber()
    {
        // Arrange
        VehicleParkingRequest request = new()
        { 
            VehicleReg = "TESTREG",
            VehicleType = 1
        };
        Space space = new()
        {
            Id = 1
        };
        _mockRepository.Setup(m => m.GetFirstAvailableSpaceAsync()).ReturnsAsync(space);
        _mockRepository.Setup(m => m.UpdateSpace(It.IsAny<Space>()));

        // Act
        var result = await _service.AddParking(request);

        // Assert
        result.Should().NotBeNull();
        result.VehicleReg.Should().Be(request.VehicleReg);
        result.SpaceNumber.Should().Be(space.Id);
        result.TimeIn.Date.Should().Be(DateTime.UtcNow.Date);
    }

    [Test]
    public async Task AddParking_WhenNoAvailableSpace_ShouldReturnNull()
    {
        // Arrange
        VehicleParkingRequest request = new()
        {
            VehicleReg = "TESTREG",
            VehicleType = 1
        };
        Space? space = null;
        _mockRepository.Setup(m => m.GetFirstAvailableSpaceAsync()).ReturnsAsync(space);

        // Act
        var result = await _service.AddParking(request);

        // Assert
        result.Should().BeNull();
        _mockRepository.Verify(m => m.UpdateSpace(It.IsAny<Space>()), Times.Never);
    }

    [Test]
    public async Task RemoveParking_WhenSuccessful_ShouldMapRegAndCharge()
    {
        // Arrange
        VehicleExitRequest request = new()
        {
            VehicleReg = "TESTREG"
        };
        Space space = new()
        {
            Id = 1,
            CurrentCarReg = "TESTREG",
            CurrentCarType = 1,
            IsOccupied = true,
            ParkingStartedTime = new DateTime(2026, 10, 10, 16, 0, 0)
        };
        _mockRepository.Setup(m => m.GetSpaceByVehicleRegAsync(It.IsAny<string>())).ReturnsAsync(space);
        _mockRepository.Setup(m => m.UpdateSpace(It.IsAny<Space>()));

        // Act
        var result = await _service.RemoveParking(request);

        // Assert
        result.Should().NotBeNull();
        result.VehicleReg.Should().NotBeNullOrEmpty();
        result.VehicleCharge.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task RemoveParking_WhenNoVehicleParked_ShouldReturnNull()
    {
        // Arrange
        VehicleParkingRequest request = new()
        {
            VehicleReg = "TESTREG",
            VehicleType = 1
        };
        Space? space = null;
        _mockRepository.Setup(m => m.GetSpaceByVehicleRegAsync(It.IsAny<string>())).ReturnsAsync(space);

        // Act
        var result = await _service.AddParking(request);

        // Assert
        result.Should().BeNull();
        _mockRepository.Verify(m => m.UpdateSpace(It.IsAny<Space>()), Times.Never);
    }
}