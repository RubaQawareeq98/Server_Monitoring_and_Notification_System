using System.Text;
using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Deserializers;
using MessageProcessingAndAnomalyDetectionService.Hubs.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class ProcessingAndAnomalyService (
    IRabbitMqMessageConsumer rabbitMqMessageConsumer,
    IDeserializer deserializer,
    ILogger<ProcessingAndAnomalyService> logger,
    IAnomalyDetector anomalyDetector,
    IServerStatisticsRepository repository,
    IServerStatisticsHub statisticsHub) : IProcessingAndAnomalyService
{
    
    public async Task RunAsync()
    {
        await rabbitMqMessageConsumer.InitializeAsync();
        await rabbitMqMessageConsumer.ConsumeMessage(
            async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var msg = Encoding.UTF8.GetString(body);

                logger.LogInformation("Received raw message {Message}:", msg);

                var serverStatistics = deserializer.Deserialize(msg);
                serverStatistics.ServerIdentifier = ea.RoutingKey;
                var alerts = await anomalyDetector.GetAlerts(serverStatistics);
                await statisticsHub.SendAlertAsync(alerts);
                await repository.InsertServerStatisticsAsync(serverStatistics);
            });
    }
}
