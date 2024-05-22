namespace CQRS.Application.Vehicles.SearchVehicles;

public sealed class AddressResponse
{
    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? City { get; init; }
    public string? Province { get; init; }
    public string? Country { get; init; }
}