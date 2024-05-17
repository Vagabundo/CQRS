using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents.Events;

public sealed record RentConfirmedEvent(Guid RentId) : IDomainEvent;