namespace TestDrivenSolutions.Domain.Entities
{
    /// <summary>
    /// Stores information about the cars that have entered the system
    /// </summary>
    public record Car : Entity
    {
        public required string Reg { get; set; }
        public int Type { get; set; }
        public required DateTime TimeIn { get; set; }
        public required DateTime TimeOut { get; set; }
        public decimal Charge { get; set; }
    }
}
