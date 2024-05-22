using CQRS.Application.Abstractions.Messaging;

namespace CQRS.Application.Rents.GetRent;

public sealed record GetRentQuery(Guid RentId) : IQuery<RentResponse>;