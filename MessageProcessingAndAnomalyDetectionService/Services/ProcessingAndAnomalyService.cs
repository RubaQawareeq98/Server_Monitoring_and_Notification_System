using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class ProcessingAndAnomalyService (IRabbitMqMessageConsumer rabbitMqMessageConsumer, IServerStatisticsRepository repository) : IProcessingAndAnomalyService
{
    
    public async Task RunAsync()
    {
       await rabbitMqMessageConsumer.ConsumeMessage(repository);
    }
}