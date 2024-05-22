using CQRS.Application.Abstractions.Messaging;

namespace CQRS.Application.Rents.RentBooking;

public record RentBookingCommand(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate

) : ICommand<Guid>;