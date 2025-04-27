namespace MessageProcessingAndAnomalyDetectionService.Services.Interfaces;

public interface IRabbitMqMessageConsumer
{
    Task ConsumeMessage();
}