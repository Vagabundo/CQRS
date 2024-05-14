namespace CQRS.Domain.Vehicles;

public record Address(
    string Street,
    string Number,
    string City,
    string Province,
    string Country
);