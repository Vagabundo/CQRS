using CQRS.Domain.Abstractions;
using CQRS.Domain.Rents.Events;
using CQRS.Domain.Shared;
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
    public DateRange? Duration { get; private set; }
    public DateTime? CreationTime { get; private set; }
    public DateTime? ConfirmationTime { get; private set; }
    public DateTime? RejectionTime { get; private set; }
    public DateTime? CancellationTime { get; private set; }
    public DateTime? CompletionTime { get; private set; }

    private Rent(){}

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

    public static Rent Book(Vehicle vehicle, Guid userId, DateRange duration, DateTime creationTime, CostService costService)
    {
        var costDetail = costService.CalculateCost(vehicle, duration);
        var rent = new Rent(
            Guid.NewGuid(),
            vehicle.Id,
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

        vehicle.LastRentDate = creationTime;
        return rent;
    }

    public Result Confirm(DateTime utcNow)
    {
        if(Status != RentStatus.Booked)
        {
            return Result.Failure(RentErrors.NotBooked);
        }

        Status = RentStatus.Booked;
        ConfirmationTime = utcNow;

        RaiseDomainEvent(new RentConfirmedEvent(Id));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if(Status != RentStatus.Booked)
        {
            return Result.Failure(RentErrors.NotBooked);
        }

        Status = RentStatus.Rejected;
        RejectionTime = utcNow;

        RaiseDomainEvent(new RentRejectedEvent(Id));

        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if(Status != RentStatus.Confirmed)
        {
            return Result.Failure(RentErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if(currentDate > Duration!.Start)
        {
            return Result.Failure(RentErrors.AlreadyStarted);
        }

        Status = RentStatus.Cancelled;
        CancellationTime = utcNow;

        RaiseDomainEvent(new RentCancelledEvent(Id));

        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if(Status != RentStatus.Confirmed)
        {
            return Result.Failure(RentErrors.NotConfirmed);
        }

        Status = RentStatus.Completed;
        CompletionTime = utcNow;

        RaiseDomainEvent(new RentCompletedEvent(Id));

        return Result.Success();
    }
}