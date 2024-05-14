using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Users.Events;

public sealed record UserCreatedEvent(Guid UserId) : IDomainEvent;