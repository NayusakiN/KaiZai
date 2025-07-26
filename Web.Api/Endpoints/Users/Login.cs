using Application.Contracts.Mediator;
using Application.Users.Login;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var command = new LoginUserCommand(request.Email, request.Password);

                Result<string> result = await mediator.SendAsync<string>(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}