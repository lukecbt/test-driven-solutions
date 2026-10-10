namespace TestDrivenSolutions.Domain.Entities
{
    /// <summary>
    /// Stores the occupied and free spaces where cars are allocated
    /// </summary>
    public record Space : Entity
    {
        public bool IsOccupied { get; set; }
        public string? CurrentVehicleReg { get; set; } // Index?
        public int? CurrentVehicleType { get; set; }
        public DateTime? TimeIn { get; set; }
    }
}
