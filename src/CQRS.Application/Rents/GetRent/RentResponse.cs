namespace CQRS.Application.Rents.GetRent;

public sealed class RentResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid VehicleId { get; init; }
    public int Status { get; init; }
    public decimal RentCost { get; init; }
    public string? RentCostCoin { get; init; }
    public decimal MaintenanceCost { get; init; }
    public string? MaintenanceCostCoin { get; init; }
    public decimal AccessoriesCost { get; init; }
    public string? AccessoriesCostCoin { get; init; }
    public decimal TotalCost { get; init; }
    public string? TotalCostCoin { get; init; }
    public DateOnly DurationStart { get; init; }
    public DateOnly DurationEnd { get; init; }
    public DateTime CreatedAt { get; init; }
}
