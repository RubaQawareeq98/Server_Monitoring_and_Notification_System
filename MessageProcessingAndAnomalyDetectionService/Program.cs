using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Services;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageProcessingAndAnomalyDetectionService;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var rabbitMqConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMqConfig>();
        IRabbitMqMessageConsumer mqMessageConsumer = new RabbitMqMessageConsumer(rabbitMqConfig);
        await mqMessageConsumer.ConsumeMessage();

    }
}