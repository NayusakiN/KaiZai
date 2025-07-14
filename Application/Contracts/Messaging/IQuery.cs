using Application.Contracts.Mediator;
using SharedKernel;

namespace Application.Contracts.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>;