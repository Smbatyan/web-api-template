using Core.Clients;
using Infrastructure.Clients;
using Infrastructure.Settings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

internal static class ServiceClientsExtension
{
    internal static IServiceCollection AddServiceClients(this IServiceCollection services)
    {
        services.AddHttpClient<IAnotherMicroserviceClient, AnotherMicroserviceClient>();

        return services;
    }

    internal static IServiceCollection AddClientSettings(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection clientSettingsConfig = configuration.GetSection("Services");
        services.Configure<ServicesClientConfigurations>(clientSettingsConfig);

        return services;
    }
}