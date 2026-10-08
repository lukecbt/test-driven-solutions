using System;

namespace TestDrivenSolutions.Domain.Models.Entities;

/// <summary>
/// Common entity for database records
/// </summary>
public record Entity
{
          public Guid Id { get; init; } = Guid.NewGuid();
          public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
          public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
