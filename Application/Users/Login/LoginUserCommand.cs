using Application.Contracts.Messaging;

namespace Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
