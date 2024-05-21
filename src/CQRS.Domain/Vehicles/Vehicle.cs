using CQRS.Domain.Abstractions;
using CQRS.Domain.Shared;

namespace CQRS.Domain.Vehicles;

public sealed class Vehicle : Entity
{
    public Model? Model { get; private set; }
    public Vin? Vin { get; private set; }
    public Address? Address { get; private set; }
    public Money Cost { get; private set; }
    public Money Maintenance { get; private set; }
    public DateTime? LastRentDate { get; internal set; }
    public List<Accessory> Accessories { get; private set; } = new();

    public Vehicle(
        Guid id, Model model, Vin vin, Address address, Money cost,
        Money maintenance, DateTime lastRentDate, List<Accessory> accessories) : base(id)
    {
        Model = model;
        Vin = vin;
        Address = address;
        Cost = cost;
        Maintenance = maintenance;
        LastRentDate = lastRentDate;
        Accessories = accessories;
    }
}
