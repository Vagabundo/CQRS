using CQRS.Domain.Vehicles;

namespace CQRS.Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}