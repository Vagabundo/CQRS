namespace CQRS.Application.Vehicles.SearchVehicles;

public sealed class VehicleResponse
{
    public Guid Id { get; init; }
    public string? Model { get; init; }
    public string? Vin { get; init; }
    public decimal Cost { get; init; }
    public string? Coin { get; init; }
    public AddressResponse? Address { get; set; }
}