using FluentAssertions;
using TestDrivenSolutions.Application.Helpers;

namespace TestDrivenSolutions.Tests.UnitTests.Helper
{
    public class CalculationHelperTests
    {
        [Test]
        public void CalculateParkingCharge_WhenSmallCarStaysFor10Minutes_ShouldReturnChargeOf2Pound()
        {
            // Arrange
            DateTime timeIn = new DateTime(2026, 10, 10, 16, 0, 0);
            DateTime timeOut = new DateTime(2026, 10, 10, 16, 10, 0);
            decimal chargePerMinute = 0.1m;

            // Act
            decimal result = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, chargePerMinute, 5, 1);

            // Assert
            result.Should().Be(3);
        }

        [Test]
        public void CalculateParkingCharge_WhenSmallCarStaysFor2Minutes_ShouldReturnChargeOf20Pence()
        {
            // Arrange
            DateTime timeIn = new DateTime(2026, 10, 10, 16, 0, 0);
            DateTime timeOut = new DateTime(2026, 10, 10, 16, 2, 0);
            decimal chargePerMinute = 0.1m;

            // Act
            decimal result = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, chargePerMinute, 5, 1);

            // Assert
            result.Should().Be(0.2m);
        }

        [Test]
        public void CalculateParkingCharge_WhenMediumCarStaysFor10Minutes_ShouldReturnChargeOf4Pound()
        {
            // Arrange
            DateTime timeIn = new DateTime(2026, 10, 10, 16, 0, 0);
            DateTime timeOut = new DateTime(2026, 10, 10, 16, 10, 0);
            decimal chargePerMinute = 0.2m;

            // Act
            decimal result = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, chargePerMinute, 5, 1);

            // Assert
            result.Should().Be(4);
        }

        [Test]
        public void CalculateParkingCharge_WhenMediumCarStaysFor2Minutes_ShouldReturnChargeOf40Pence()
        {
            // Arrange
            DateTime timeIn = new DateTime(2026, 10, 10, 16, 0, 0);
            DateTime timeOut = new DateTime(2026, 10, 10, 16, 2, 0);
            decimal chargePerMinute = 0.2m;

            // Act
            decimal result = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, chargePerMinute, 5, 1);

            // Assert
            result.Should().Be(0.4m);
        }


        [Test]
        public void CalculateParkingCharge_WhenLargeCarStaysFor10Minutes_ShouldReturnChargeOf6Pound()
        {
            // Arrange
            DateTime timeIn = new DateTime(2026, 10, 10, 16, 0, 0);
            DateTime timeOut = new DateTime(2026, 10, 10, 16, 10, 0);
            decimal chargePerMinute = 0.4m;

            // Act
            decimal result = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, chargePerMinute, 5, 1);

            // Assert
            result.Should().Be(6);
        }

        [Test]
        public void CalculateParkingCharge_WhenLargeCarStaysFor2Minutes_ShouldReturnChargeOf80Pence()
        {
            // Arrange
            DateTime timeIn = new DateTime(2026, 10, 10, 16, 0, 0);
            DateTime timeOut = new DateTime(2026, 10, 10, 16, 2, 0);
            decimal chargePerMinute = 0.4m;

            // Act
            decimal result = CalculationHelper.CalculateParkingCharge(timeIn, timeOut, chargePerMinute, 5, 1);

            // Assert
            result.Should().Be(0.8m);
        }
    }
}
