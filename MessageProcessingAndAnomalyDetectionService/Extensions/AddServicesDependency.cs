using MessageProcessingAndAnomalyDetectionService.AnomalyDetection;
using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Deserializers;
using MessageProcessingAndAnomalyDetectionService.Factories;
using MessageProcessingAndAnomalyDetectionService.Factories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Hubs;
using MessageProcessingAndAnomalyDetectionService.Hubs.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Repositories;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Services;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace MessageProcessingAndAnomalyDetectionService.Extensions;

public static class AddServicesDependency
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Information));
    
        
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IRabbitMqMessageConsumer, RabbitMqMessageConsumer>();
        services.AddSingleton<IDeserializer, JsonDeserializer>();
        services.AddSingleton<IServerStatisticsHub, ServerStatisticsHub>();
    
        services.AddSingleton<IMongoDatabase>(provider => 
        {
            var mongoConfig = provider.GetRequiredService<IOptions<MongoDbConfig>>();
            var mongo = new MongoDbServerDatabase(mongoConfig);
            return mongo.GetDatabase();
        });
        services.AddSingleton<IServerStatisticsRepository, ServerStatisticsRepository>();

        services.AddSingleton<IAnomalyChecker, CpuAnomalyChecker>();
        services.AddSingleton<IAnomalyChecker, MemoryAnomalyChecker>();
        services.AddSingleton<IAnomalyChecker, MemoryHighUsageChecker>();
        services.AddSingleton<IAnomalyChecker, CpuHighUsageAlert>();

        services.AddSingleton<IAnomalyDetector, AnomalyDetector>();
        services.AddSingleton<IProcessingAndAnomalyService, ProcessingAndAnomalyService>();
    }
}
