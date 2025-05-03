using RabbitMQ.Client.Events;

namespace MessageProcessingAndAnomalyDetectionService.Services.Interfaces;

public interface IRabbitMqMessageConsumer
{
    Task InitializeAsync();
    Task ConsumeMessage(AsyncEventHandler<BasicDeliverEventArgs> onMessageReceived);
}