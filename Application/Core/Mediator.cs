using Application.Contracts.Mediator;
using Application.Contracts.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Application.Core;

internal class Mediator(IServiceProvider provider) : IMediator
{
	public async Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
		where TCommand : ICommand<TResult>
	{
		var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

		var behaviors = provider.GetServices<IPipelineBehavior<TCommand, TResult>>().Reverse();
		Func<Task<Result<TResult>>> handlerDelegate = () => handler.HandleAsync(command, cancellationToken);
		foreach (var behavior in behaviors)
		{
			var next = handlerDelegate;
			handlerDelegate = () => behavior.HandleAsync(command, next, cancellationToken);
		}

		return await handlerDelegate();
	}
	
	public async Task<Result> SendCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
		where TCommand : ICommand
	{
		var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();

		var behaviors = provider.GetServices<IPipelineBehavior<TCommand>>().Reverse();
		Func<Task<Result>> handlerDelegate = () => handler.HandleAsync(command, cancellationToken);
		foreach (var behavior in behaviors)
		{
			var next = handlerDelegate;
			handlerDelegate = () => behavior.HandleAsync(command, next, cancellationToken);
		}

		return await handlerDelegate();
	}

	public async Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
		where TQuery : IQuery<TResult>
	{
		var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

		var behaviors = provider.GetServices<IPipelineBehavior<TQuery, TResult>>().Reverse();
		Func<Task<Result<TResult>>> handlerDelegate = () => handler.HandleAsync(query, cancellationToken);
		foreach (var behavior in behaviors)
		{
			var next = handlerDelegate;
			handlerDelegate = () => behavior.HandleAsync(query, next, cancellationToken);
		}

		return await handlerDelegate();
	}
}