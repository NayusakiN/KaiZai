using SharedKernel;

namespace Application.Contracts.Mediator;

public interface IPipelineBehavior<in TInput, TOutput>
{
    Task<Result<TOutput>> HandleAsync(TInput input, Func<Task<Result<TOutput>>> next, CancellationToken cancellationToken = default);
}