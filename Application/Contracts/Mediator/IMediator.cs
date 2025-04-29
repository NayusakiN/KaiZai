using Application.Contracts.Messaging;
using SharedKernel;

namespace Application.Contracts.Mediator;

public interface IMediator
{
	Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
		where TCommand : ICommand<TResult>;

	Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
		where TQuery : IQuery<TResult>;
}