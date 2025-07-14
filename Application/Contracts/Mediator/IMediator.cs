using Application.Contracts.Messaging;
using SharedKernel;

namespace Application.Contracts.Mediator;

public interface IMediator
{
	Task<Result<TResult>> SendAsync<TResult>(IRequest request, CancellationToken cancellationToken = default);
}