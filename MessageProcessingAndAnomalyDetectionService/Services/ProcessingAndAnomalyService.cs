using System.Text;
using MessageProcessingAndAnomalyDetectionService.Deserializers;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class ProcessingAndAnomalyService (
    IRabbitMqMessageConsumer rabbitMqMessageConsumer,
    IServerStatisticsRepository repository,
    IDeserializer deserializer,
    ILogger logger) : IProcessingAndAnomalyService
{
    
    public async Task RunAsync()
    {
        await rabbitMqMessageConsumer.InitializeAsync();
        
        await rabbitMqMessageConsumer.ConsumeMessage(
            async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var msg = Encoding.UTF8.GetString(body);

                logger.LogInformation("Received raw message: {Message}", msg);
                logger.LogInformation("Routing key: {RoutingKey}", ea.RoutingKey);

                var serverStatistics = deserializer.Deserialize(msg);
                serverStatistics.ServerIdentifier = ea.RoutingKey;

                await repository.InsertServerStatisticsAsync(serverStatistics);
            });
    }
}
