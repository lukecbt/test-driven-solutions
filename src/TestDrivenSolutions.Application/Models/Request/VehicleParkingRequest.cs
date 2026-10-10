namespace TestDrivenSolutions.Application.Models.Request
{
    public class VehicleParkingRequest
    {
        public required string VehicleReg { get; init; }
        public required int VehicleType { get; init; }
    }
}
