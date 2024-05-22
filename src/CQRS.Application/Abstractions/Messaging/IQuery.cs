using CQRS.Domain.Abstractions;
using MediatR;

namespace CQRS.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}