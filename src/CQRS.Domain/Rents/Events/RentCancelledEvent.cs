using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents.Events;

public sealed record RentCancelledEvent(Guid Id) : IDomainEvent;

