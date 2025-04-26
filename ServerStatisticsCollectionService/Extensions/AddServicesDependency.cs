using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Factories;
using ServerStatisticsCollectionService.Factories.Interfaces;
using ServerStatisticsCollectionService.Models;
using ServerStatisticsCollectionService.Serializers;
using ServerStatisticsCollectionService.Services;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Extensions;

public static class AddServicesDependency
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var serverStatisticsConfig = configuration.GetSection("ServerStatisticsConfig").Get<ServerStatisticsConfig>();
        var rabbitMqConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMqConfig>();
        
        services.AddSingleton(serverStatisticsConfig);
        services.AddSingleton(rabbitMqConfig);
        services.AddSingleton<IServerStatisticsCollectorFactory, ServerStatisticsCollectorFactory>();
        services.AddSingleton<IServerStatisticsCollector>(provider =>
        {
            var factory = provider.GetRequiredService<IServerStatisticsCollectorFactory>();
            return factory.GetServerStatisticsCollector(PlatformID.Win32S);
        });
        services.AddSingleton<IStatisticsCollector, StatisticsCollector>();
        services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();
        services.AddSingleton<ISerializer<ServerStatistics>, JsonSerializer<ServerStatistics>>();
        services.AddSingleton<IServerStatisticsPublisher, ServerStatisticsPublisher>();
    }
}
