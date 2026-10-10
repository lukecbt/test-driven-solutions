namespace TestDrivenSolutions.Domain.Entities
{
    /// <summary>
    /// Type for holding pricing information per car type
    /// </summary>
    public record CarType : Entity
    {
        public decimal CostPerMinute { get; set; }
        public string? Description { get; set; }
    }
}
