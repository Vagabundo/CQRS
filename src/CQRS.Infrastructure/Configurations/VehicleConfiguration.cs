using CQRS.Domain.Shared;
using CQRS.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Infrastructure.Configurations;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");
        builder.HasKey(vehicle => vehicle.Id);

        // OwnsOne for 1 to many conversions
        builder.OwnsOne(vehicle => vehicle.Address);

        // Property for 1 to 1 conversions
        builder.Property(vehicle => vehicle.Model)
            .HasMaxLength(200)
            .HasConversion(model => model!.Value, value => new Model(value));
        
        builder.Property(vehicle => vehicle.Vin)
            .HasMaxLength(500)
            .HasConversion(vin => vin!.Value, value => new Vin(value));

        builder.OwnsOne(vehicle => vehicle.Cost, costBuilder => {
            costBuilder.Property(money => money.Coin)
                .HasConversion(coin => coin.Code, code => Coin.FromCode(code!));
        });

        builder.OwnsOne(vehicle => vehicle.Maintenance, maintenanceBuilder => {
            maintenanceBuilder.Property(money => money.Coin)
                .HasConversion(coin => coin.Code, code => Coin.FromCode(code!));
        });
    }
}