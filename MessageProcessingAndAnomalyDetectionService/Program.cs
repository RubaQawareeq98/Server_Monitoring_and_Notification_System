using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Extensions;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MessageProcessingAndAnomalyDetectionService;

internal abstract class Program
{
    private static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        services.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));
        services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMQConfig"));
        services.Configure<AnomalyDetectionConfig>(configuration.GetSection("AnomalyDetectionConfig"));
        services.Configure<SignalRConfig>(configuration.GetSection("SignalRConfig"));

        services.AddServices(configuration);
        
        var serviceProvider = services.BuildServiceProvider();

        try
        {
            var processingService = serviceProvider.GetRequiredService<IProcessingAndAnomalyService>();
            await processingService.RunAsync();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Application startup failed.");
        }
    }
}