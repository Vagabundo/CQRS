using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents.Events;

public sealed record RentRejectedEvent(Guid Id) : IDomainEvent;

