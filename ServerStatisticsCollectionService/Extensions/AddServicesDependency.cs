using Domain.Configurations;
using Domain.Models;
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
        var serverStatisticsConfig = new ServerStatisticsConfig
        {
            ServerIdentifier = ""
        };
        Console.WriteLine("ServerStatisticsCollectionService is starting...");
        configuration.Bind("ServerStatisticsConfig", serverStatisticsConfig);

        var rabbitMqConfig = new RabbitMqConfig
        {
            HostName = null,
            UserName = null,
            Password = null,
            Exchange = null,
            QueueName = null
        };
        configuration.Bind("RabbitMQConfig", rabbitMqConfig);

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
