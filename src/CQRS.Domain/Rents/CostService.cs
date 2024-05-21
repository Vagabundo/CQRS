using CQRS.Domain.Shared;
using CQRS.Domain.Vehicles;

namespace CQRS.Domain.Rents;

public class CostService
{
    public CostDetail CalculateCost(Vehicle vehicle, DateRange period)
    {
        var coin = vehicle.Cost.Coin;
        var costPerPeriod = new Money(period.Days * vehicle.Cost.Ammount, coin);

        decimal percentageChange = 0;

        foreach (var accessory in vehicle.Accessories)
        {
            percentageChange += accessory switch
            {
                Accessory.AppleCar or Accessory.AndroidCar => 0.05m,
                Accessory.AirConditioner => 0.01m,
                Accessory.Maps => 0.01m,
                _ => 0
            };
        }

        var accessoryCharges = Money.Zero(coin);

        if (percentageChange > 0)
        {
            accessoryCharges = new Money(
                costPerPeriod.Ammount*percentageChange,
                coin
            );
        }

        var totalCost = Money.Zero();
        totalCost += costPerPeriod;

        if(!vehicle.Maintenance.IsZero())
        {
            totalCost += vehicle.Maintenance;
        }

        totalCost += accessoryCharges;

        return new CostDetail(costPerPeriod, vehicle.Maintenance, accessoryCharges, totalCost);
    }
}