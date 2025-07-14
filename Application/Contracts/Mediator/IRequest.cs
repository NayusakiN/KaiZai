namespace Application.Contracts.Mediator;

public interface IRequest { }

public interface IRequest<TResult> : IRequest { }