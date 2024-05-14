using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Vehicles;

public sealed class Vehicle : Entity
{
    public string? Model { get; private set; }
    public string? Vin { get; private set; }
    public string? Street { get; private set; }
    public int Number { get; private set; }
    public string? City { get; private set; }
    public string? Province { get; private set; }
    public string? Country { get; private set; }
    public decimal Price { get; private set; }
    public string? Coin { get; private set; }
    public decimal Mantainance { get; private set; }
    public string? MantainanceCoin { get; private set; }
    public DateTime? LastRentDate { get; private set; }
    public List<Accessory> Accessories { get; private set; } = new();

    public Vehicle(Guid id) : base(id)
    {}
}
