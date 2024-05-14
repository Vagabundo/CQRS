using CQRS.Domain.Abstractions;
using CQRS.Domain.Rents.Events;
using CQRS.Domain.Vehicles;

namespace CQRS.Domain.Rents;

public sealed class Rent : Entity
{
    public Guid VehicleId { get; private set; }
    public Guid UserId { get; private set; }
    public Money? CostPerPeriod { get; private set; }
    public Money? Maintenance { get; private set; }
    public Money? Accessories { get; private set; }
    public Money? TotalCost { get; private set; }
    public RentStatus Status { get; private set; }
    public DateRange Duration { get; private set; }
    public DateTime? CreationTime { get; private set; }
    public DateTime? ConfirmationTime { get; private set; }
    public DateTime? RejectionTime { get; private set; }
    public DateTime? CancellationTime { get; private set; }
    public DateTime? CompletionTime { get; private set; }


    public Rent(
        Guid id, Guid vehicleId, Guid userId, DateRange duration, Money costPerPeriod,
        Money maintenance, Money accessories, Money totalCost, RentStatus status, DateTime creationTime) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration = duration;
        CostPerPeriod = costPerPeriod;
        Maintenance = maintenance;
        Accessories = accessories;
        TotalCost = totalCost;
        Status = status;
        CreationTime = creationTime;
    }

    public static Rent Book(Guid vehicleId, Guid userId, DateRange duration, DateTime creationTime, CostDetail costDetail)
    {
        var rent = new Rent(
            Guid.NewGuid(),
            vehicleId,
            userId,
            duration,
            costDetail.CostPerPeriod,
            costDetail.Maintenance,
            costDetail.Accessories,
            costDetail.TotalCost,
            RentStatus.Booked,
            creationTime
        );

        rent.RaiseDomainEvent(new RentBookingEvent(rent.Id));

        return rent;
    }
}