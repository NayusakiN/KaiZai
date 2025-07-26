using Application.Categories.Create;
using Application.Categories.Update;
using Application.Contracts.Data;
using Application.Contracts.Mediator;
using Application.Contracts.Messaging;
using Application.Core;
using Application.Core.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register CQRS mediator
        services.AddSingleton<IMediator, Mediator>();
        
        // Register pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        //TODO: Register handlers automatically
        // Register command handlers
        services.AddTransient<ICommandHandler<CreateCategoryCommand, Guid>, CreateCategoryCommandHandler>();
        services.AddTransient<ICommandHandler<UpdateCategoryCommand, Unit>, UpdateCategoryCommandHandler>();
        
        return services;
    }
}