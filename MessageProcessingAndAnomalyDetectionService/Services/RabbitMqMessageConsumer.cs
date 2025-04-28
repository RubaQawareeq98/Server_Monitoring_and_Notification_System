using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Factories.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class RabbitMqMessageConsumer(IChannelFactory channelFactory, IOptions<RabbitMqConfig> options) : IRabbitMqMessageConsumer
{
    private IChannel? _channel;
    private readonly RabbitMqConfig _rabbitMqConfig = options.Value;

    
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
            queue: _rabbitMqConfig.QueueName,
            autoAck: false,
            consumer: consumer);
        
        await Task.Delay(Timeout.Infinite);
    }
}
