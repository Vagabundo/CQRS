namespace CQRS.Api.Controllers.Rents;

public sealed record BookingRentRequest(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);