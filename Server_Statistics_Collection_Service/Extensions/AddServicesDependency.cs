using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server_Statistics_Collection_Service.Configurations;
using Server_Statistics_Collection_Service.Models;
using Server_Statistics_Collection_Service.Serializers;
using Server_Statistics_Collection_Service.Services;
using Server_Statistics_Collection_Service.Services.Interfaces;
using Server_Statistics_Collection_Service.Strategies;

namespace Server_Statistics_Collection_Service.Extensions;

public static class AddServicesDependency
{
    public static void AddServices(this IServiceCollection service, IConfiguration configuration)
    {
        var serverStatisticsConfig = configuration.GetSection("ServerStatisticsConfig").Get<ServerStatisticsConfig>();
        var rabbitMqConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMqConfig>();

        if (serverStatisticsConfig is null || rabbitMqConfig is null)
        {
            Console.WriteLine("No server statistics config found.");
            return;
        }
        
        service.AddSingleton(serverStatisticsConfig);
        service.AddSingleton(rabbitMqConfig);
        service.AddSingleton<IServerStatisticsCollector, WindowsServerStatisticsCollector>();
        service.AddSingleton<IStatisticsCollector, StatisticsCollector>();
        service.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();
        service.AddSingleton<ISerializer<ServerStatistics>, JsonSerializer<ServerStatistics>>();
        service.AddSingleton<IServerStatisticsPublisher, ServerStatisticsPublisher>();
    }
}