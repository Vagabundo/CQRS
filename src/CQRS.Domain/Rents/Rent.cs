using CQRS.Domain.Abstractions;

namespace CQRS.Domain.Rents;

public sealed class Rent : Entity
{
    public RentStatus Status { get; private set; }

    
    public Rent(Guid id) : base(id)
    {
    }
}