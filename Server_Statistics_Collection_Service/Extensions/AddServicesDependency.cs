using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server_Statistics_Collection_Service.Configurations;
using Server_Statistics_Collection_Service.Factories;
using Server_Statistics_Collection_Service.Factories.Interfaces;
using Server_Statistics_Collection_Service.Models;
using Server_Statistics_Collection_Service.Serializers;
using Server_Statistics_Collection_Service.Services;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Extensions;

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
