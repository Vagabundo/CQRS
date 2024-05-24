using Bogus;
using CQRS.Application.Abstractions.Data;
using CQRS.Domain.Vehicles;
using Dapper;

namespace CQRS.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> vehicles = new();

        for (var i = 0; i < 100; i++)
        {
            vehicles.Add(new {
                Id = Guid.NewGuid(),
                Model = faker.Vehicle.Model(),
                Vin = faker.Vehicle.Vin(),
                Street = faker.Address.StreetName(),
                Number = faker.Random.Number(1, 200),
                City = faker.Address.City(),
                Province = faker.Address.County(),
                Country = faker.Address.Country(),
                CostAmmount = Math.Round(faker.Random.Decimal(1000, 20000), 2),
                CostCoin = "EUR",
                MaintenanceAmmount = Math.Round(faker.Random.Decimal(100, 200), 2),
                MaintenanceCoin = "EUR",
                LastRentDate = DateTime.MinValue,
                Accessories = new List<int> { (int)Accessory.Wifi, (int)Accessory.AppleCar }            
            });
        }

        const string sql = """
                INSERT INTO public.vehicles
                    (id, model, vin, address_street, address_number, address_city, address_province, address_country, cost_ammount, cost_coin, maintenance_ammount, maintenance_coin, last_rent_date, accessories)
                VALUES(@Id, @Model, @Vin, @Street, @Number, @City, @Province, @Country, @CostAmmount, @CostCoin, @MaintenanceAmmount, @MaintenanceCoin, @LastRentDate, @Accessories)
        """;

        connection.Execute(sql, vehicles);
    }
}
