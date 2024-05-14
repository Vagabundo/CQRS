namespace CQRS.Domain.Rents;

public enum RentStatus
{
    Booked,
    Confirmed,
    Rejected,
    Cancelled,
    Completed
}