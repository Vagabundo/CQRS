using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents.Events;

public sealed record RentCompletedEvent(Guid Id) : IDomainEvent;

