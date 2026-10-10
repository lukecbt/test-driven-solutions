namespace TestDrivenSolutions.Domain.Entities
{
    /// <summary>
    /// Type for holding pricing information per car type
    /// </summary>
    public record VehicleTypeTarrif : Entity
    {
        public decimal CostPerMinute { get; set; }
        public int AdditionalChargeMinutes { get; set; }
        public decimal AdditionalChargeAmount { get; set; }
        public string? Description { get; set; }
    }
}
