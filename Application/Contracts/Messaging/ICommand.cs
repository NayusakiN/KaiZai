using Application.Contracts.Mediator;
using SharedKernel;

namespace Application.Contracts.Messaging;

public interface ICommand<TResponse> : IRequest<TResponse>;