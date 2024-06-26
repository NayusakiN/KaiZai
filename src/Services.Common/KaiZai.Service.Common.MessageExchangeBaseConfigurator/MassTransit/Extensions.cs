using System.Reflection;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;

namespace KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;

public static class Extensions
{
    /// <summary>
    /// Adds MassTransit configuration with a base setup to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks> 
    /// Important! Do not forget to configure ServiceSettings and RabbitMQSettings sections in your project.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which MassTransit configuration will be added.</param>
    /// <param name="serviceSettings">The settings related to the microservice.</param>
    /// <param name="rabbitMQSettings">The settings related to the RabbitMQ message broker.</param>
    /// <param name="rabbitMqAdditionalConfigurations">An optional action for additional MassTransit and RabbitMQ configurations.</param>
    /// <param name="assembliesConsumers">An array of assemblies containing MassTransit consumers. 
    /// If not provided, the entry assembly will be used as default value.
    /// Attention!!! Default value will be null when called from unmanaged code.
    /// </param>
    /// <returns>The modified <see cref="IServiceCollection"/> with MassTransit configuration.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceSettings"/> or <paramref name="rabbitMQSettings"/> is null.</exception>
    public static IServiceCollection AddMassTransitCoreSetUp(this IServiceCollection services,
        ServiceSettings serviceSettings, 
        RabbitMQSettings rabbitMQSettings,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqAdditionalConfigurations = null,
        params Assembly[] assembliesConsumers)
    {
        if (serviceSettings == null)
        {
            throw new ArgumentNullException($"{nameof(serviceSettings)} is missing in the configuration.");
        }

        services.AddMassTransit(configure => 
        {
            assembliesConsumers = assembliesConsumers.Any() == true ? assembliesConsumers : new Assembly[] { Assembly.GetEntryAssembly() };
            configure.AddConsumers(assemblies: assembliesConsumers);
            configure.UsingRabbitMq((context, configurator) => 
            {
                if (rabbitMQSettings == null)
                {
                    throw new ArgumentNullException($"{nameof(serviceSettings)} is missing in the configuration.");
                }
                configurator.Host(rabbitMQSettings.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                configurator.UseMessageRetry(retryConfigurator => 
                {
                    retryConfigurator.Interval(3, TimeSpan.FromSeconds(3));
                });
                if (rabbitMqAdditionalConfigurations != null)
                {
                    rabbitMqAdditionalConfigurations(context, configurator);
                }
            });
        });

        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

        return services;
    }
}