using CQRS.Domain.Vehicles;

namespace CQRS.Domain.Rents;

public interface IRentRepository
{
    Task<Rent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange duration, CancellationToken cancellationToken = default);
    void Add(Rent rent);
}