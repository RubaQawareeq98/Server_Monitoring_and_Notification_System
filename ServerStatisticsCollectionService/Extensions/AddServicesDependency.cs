using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Factories;
using ServerStatisticsCollectionService.Factories.Interfaces;
using ServerStatisticsCollectionService.Serializers;
using ServerStatisticsCollectionService.Services;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Extensions;

public static class AddServicesDependency
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var serverStatisticsConfig = configuration.GetSection("ServerStatisticsConfig").Get<ServerStatisticsConfig>() ?? throw new InvalidCastException("ServerStatisticsConfig is missing");
        var rabbitMqConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMqConfig>() ?? throw new InvalidCastException("RabbitMqConfig is missing");
        
        services.AddSingleton(serverStatisticsConfig);
        services.AddSingleton(rabbitMqConfig);
        services.AddSingleton<IServerStatisticsCollectorFactory, ServerStatisticsCollectorFactory>();
        services.AddSingleton<IServerStatisticsCollector>(provider =>
        {
            var factory = provider.GetRequiredService<IServerStatisticsCollectorFactory>();
            return factory.GetServerStatisticsCollector(PlatformID.Win32S);
        });
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IStatisticsCollector, StatisticsCollector>();
        services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();
        services.AddSingleton<ISerializer<ServerStatistics>, JsonSerializer<ServerStatistics>>();
        services.AddSingleton<IServerStatisticsPublisher, ServerStatisticsPublisher>();
    }
}
