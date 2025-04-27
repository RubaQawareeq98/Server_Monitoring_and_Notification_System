using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class MessageProcessingAndAnomalyDetectionService (IRabbitMqMessageConsumer rabbitMqMessageConsumer) : IMessageProcessingAndAnomalyDetectionService
{
    
    public async Task RunAsync()
    {
       await rabbitMqMessageConsumer.ConsumeMessage();
    }
}