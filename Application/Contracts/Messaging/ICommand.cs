using SharedKernel;

namespace Application.Contracts.Messaging;

public interface ICommand: IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;

public interface IBaseCommand;