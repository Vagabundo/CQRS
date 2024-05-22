namespace CQRS.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime CurrentTime { get; }
}