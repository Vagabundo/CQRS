using CQRS.Application.Abstractions.Messaging;

namespace CQRS.Application.Vehicles.SearchVehicles;

public sealed record SearchVehiclesQuery(DateOnly StartDate, DateOnly EndDate) : IQuery<IReadOnlyList<VehicleResponse>>;