using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Vehicles;

public static class VehicleErrors
{
    public static Error NotFound = new("Vehicle.NotFound", "No vehicle found by given Id");
}