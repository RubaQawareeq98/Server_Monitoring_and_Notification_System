using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Factories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class RabbitMqMessageConsumer(IChannelFactory channelFactory, RabbitMqConfig rabbitMqConfig) : IRabbitMqMessageConsumer
{
    private IChannel? _channel;
    
    public async Task InitializeAsync()
    {
        _channel = await channelFactory.GetChannel();
    }
    
    public async Task ConsumeMessage(AsyncEventHandler<BasicDeliverEventArgs> onMessageReceived)
    {
        ArgumentNullException.ThrowIfNull(_channel);
        
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += onMessageReceived;

        await _channel.BasicConsumeAsync(
            queue: rabbitMqConfig.QueueName,
            autoAck: false,
            consumer: consumer);

        await Task.Delay(100);
    }
}
