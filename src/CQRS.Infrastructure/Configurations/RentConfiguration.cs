using CQRS.Domain.Rents;
using CQRS.Domain.Shared;
using CQRS.Domain.Users;
using CQRS.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Infrastructure.Configurations;

internal sealed class RentConfiguration : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.ToTable("rents");
        builder.HasKey(rent => rent.Id);

        builder.OwnsOne(rent => rent.CostPerPeriod, costBuilder => {
            costBuilder.Property(money => money.Coin)
                .HasConversion(coin => coin.Code, code => Coin.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Maintenance, maintenanceCostBuilder => {
            maintenanceCostBuilder.Property(money => money.Coin)
                .HasConversion(coin => coin.Code, code => Coin.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Accessories, accessoriesCostBuilder => {
            accessoriesCostBuilder.Property(money => money.Coin)
                .HasConversion(coin => coin.Code, code => Coin.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.TotalCost, totalCostBuilder => {
            totalCostBuilder.Property(money => money.Coin)
                .HasConversion(coin => coin.Code, code => Coin.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Duration);

        // a rent has only one vehicle, a vehicle can be in many rents
        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(rent => rent.VehicleId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rent => rent.UserId);
    }
}