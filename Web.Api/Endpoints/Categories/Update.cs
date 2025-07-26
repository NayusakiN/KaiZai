using Application.Categories.Update;
using Application.Contracts.Mediator;
using Application.Core;
using Domain.Categories;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
using Web.Api.Mappings;

namespace Web.Api.Endpoints.Categories;

internal sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Icon Icon { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public CategoryType CategoryType { get; set; }
    }
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{id:guid}/update", async (Request request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.SendAsync<Unit>(request.ToCommand(), cancellationToken);
                    
                if (!result.IsSuccess)
                {
                    Console.WriteLine($"Command !Success");
                }
                
                // if (result.GetValue() is Result<Unit> userResultValue)
                // {
                //     return userResultValue.Match(Results.NoContent, CustomResults.Problem);
                // }
                
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Categories)
            .RequireAuthorization();
    }
}