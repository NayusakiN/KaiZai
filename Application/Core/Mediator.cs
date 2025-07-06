using System.Collections.Concurrent;
using System.Reflection;
using Application.Contracts.Mediator;
using Application.Contracts.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Application.Core;

internal class Mediator(IServiceProvider provider) : IMediator
{
    private static readonly ConcurrentDictionary<(Type RequestType, Type ResultType), MethodInfo> _cachedHandlers =
        new();

    // Internal placeholder methods that would actually contain the dispatch logic to handlers.
    public async Task<Result<TResult>> SendAsync<TResult>(IRequest request,
        CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var expectedTResult = typeof(TResult);

        // Create a unique key for caching based on the request type and the expected TResult.
        var cacheKey = (requestType, expectedTResult);

        // 1. Get the closed generic method from cache, or create it via reflection and add to cache.
        // This logic is now encapsulated in a separate helper method.
        var closedGenericMethod = _cachedHandlers.GetOrAdd(cacheKey,
            key => CreateAndCacheHandlerMethod(key.RequestType, key.ResultType));

        // Invoke the determined method using reflection.
        // This method invocation effectively calls either SendCommandAsync<TCommand, TResult>
        // or SendQueryAsync<TQuery, TResult>.
        object? taskAsObject = closedGenericMethod.Invoke(this, [request, cancellationToken]);

        // Cast the result of the invocation to the expected Task type.
        var resultTask = (Task<Result<TResult>>)taskAsObject!;

        // Await the task returned by the handler/pipeline and return its final result.
        return await resultTask;
    }

    /// <summary>
    /// Discovers the appropriate generic SendCommandInternal or SendQueryInternal method via reflection,
    /// validates it, creates a closed generic method, and returns it for caching.
    /// </summary>
    /// <param name="requestType">The runtime type of the IRequest (e.g., CreateUserCommand).</param>
    /// <param name="expectedTResult">The TResult type expected by the public SendAsync call.</param>
    /// <returns>A closed generic MethodInfo ready for invocation.</returns>
    /// <exception cref="InvalidOperationException">If handler method cannot be found or result types mismatch.</exception>
    private MethodInfo CreateAndCacheHandlerMethod(Type requestType, Type expectedTResult)
    {
        Type? resultTypeFromRequestInterface = null;
        MethodInfo? openGenericMethodDefinition = null;
        string? targetMethodName = null; // To store name of method to find (e.g., SendCommandInternal)

        // Determine if it's a Command or a Query based on implemented interfaces
        var commandInterface = requestType.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>));

        if (commandInterface != null)
        {
            resultTypeFromRequestInterface = commandInterface.GetGenericArguments()[0];
            targetMethodName = nameof(SendCommandInternalAsync); // Target the new internal method
        }
        else
        {
            var queryInterface = requestType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>));

            if (queryInterface == null)
            {
                // If the request doesn't implement either ICommand or IQuery, it's an invalid type.
                throw new InvalidOperationException(
                    $"Request type {requestType.Name} does not implement ICommand<TResult> or IQuery<TResult>.");
            }

            resultTypeFromRequestInterface = queryInterface.GetGenericArguments()[0];
            targetMethodName = nameof(SendQueryInternalAsync); // Target the new internal method
        }

        // Find the correct open generic method definition (e.g., SendCommandInternal<,>)
        openGenericMethodDefinition = typeof(Mediator).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(m => m is { Name: not null, IsGenericMethodDefinition: true } &&
                                 m.Name == targetMethodName && // Match the specific internal method name
                                 m.GetGenericArguments().Length ==
                                 2); // Ensure it's the correct overload (TRequest, TResult)

        if (openGenericMethodDefinition == null)
        {
            throw new InvalidOperationException(
                $"Could not find appropriate open generic method via reflection for request type {requestType.Name} (target: {targetMethodName}). " +
                "Ensure the internal SendCommandInternal/SendQueryInternal methods exist with 2 generic arguments and correct access modifiers (private instance methods).");
        }

        // Important Validation: Ensure the TResult expected by SendAsync matches the TResult declared by the request's interface.
        if (resultTypeFromRequestInterface != expectedTResult)
        {
            throw new InvalidOperationException(
                $"The TResult specified in SendAsync<{expectedTResult.Name}> does not match the TResult declared by the request {requestType.Name} (which is {resultTypeFromRequestInterface?.Name}). " +
                "Ensure your request interface (ICommand<TResult> or IQuery<TResult>) declares the correct result type.");
        }

        // Create the specific, callable method (e.g., SendCommandInternal<CreateUserCommand, UserCreatedResult>).
        var closedGenericMethod =
            openGenericMethodDefinition.MakeGenericMethod(requestType, resultTypeFromRequestInterface);

        return closedGenericMethod;
    }

    private async Task<Result<TResult>> SendCommandInternalAsync<TCommand, TResult>(TCommand command,
        CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>
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

    private async Task<Result<TResult>> SendQueryInternalAsync<TQuery, TResult>(TQuery query,
        CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
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