namespace TestDrivenSolutions.Domain.Entities
{
    /// <summary>
    /// Stores information about the cars that have entered the system
    /// </summary>
    public record Car : Entity
    {
        public required string Reg { get; set; }
        public int Type { get; set; }
        public required DateTime ArrivalTime { get; set; }
        public required DateTime DepartureTime { get; set; }
        public decimal Charge { get; set; }
    }
}
