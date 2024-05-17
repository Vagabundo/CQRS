using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents;

public static class RentErrors
{
    public static Error NotFound = new Error(
        "Rent.NotFound",
        "Rent with specified Id was not found"
    );

    public static Error Overlap = new Error(
        "Rent.Overlap",
        "The rent is being taken by 2 or more costumers for the same date"
    );

    public static Error NotBooked = new Error(
        "Rent.NotBooked",
        "The rent is not booked"
    );

    public static Error NotConfirmed = new Error(
        "Rent.NotConfirmed",
        "The rent is not confirmed"
    );

    public static Error AlreadyStarted = new Error(
        "Rent.AlreadyStarted",
        "The rent is already started"
    );
}