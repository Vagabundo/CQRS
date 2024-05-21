using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Reviews.Events;

public sealed record ReviewCreatedEvent(Guid reviewId) : IDomainEvent;