namespace TestDrivenSolutions.Application.Models.Response
{
    public class VehicleExitResponse
    {
        public string VehicleReg { get; init; }
        public double VehicleCharge { get; init; }
        public DateTime TimeIn { get; init; }
        public DateTime TimeOut { get; init; }
    }
}
