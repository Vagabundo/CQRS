using CQRS.Domain.Rents;
using CQRS.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Repositories;

internal sealed class RentRepository : Repository<Rent>, IRentRepository
{
    private static readonly RentStatus[] ActiveRentStatuses = { RentStatus.Booked, RentStatus.Confirmed, RentStatus.Completed };

    public RentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange duration, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rent>()
                .AnyAsync(rent =>
                    rent.VehicleId == vehicle.Id &&
                    rent.Duration!.Start <= duration.End &&
                    rent.Duration.End >= duration.Start &&
                    ActiveRentStatuses.Contains(rent.Status),
                    cancellationToken
                );
    }
}