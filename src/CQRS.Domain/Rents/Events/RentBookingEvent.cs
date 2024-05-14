using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents.Events;

public sealed record RentBookingEvent(Guid rentId) : IDomainEvent;
