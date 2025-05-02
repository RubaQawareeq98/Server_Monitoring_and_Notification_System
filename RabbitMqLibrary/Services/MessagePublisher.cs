using RabbitMQ.Client;
using RabbitMqLibrary.Factories.Interfaces;
using RabbitMqLibrary.Models;
using RabbitMqLibrary.Serializers;
using RabbitMqLibrary.Services.Interfaces;

namespace RabbitMqLibrary.Services;

public class MessagePublisher(
    IChannelFactory channelFactory,
    RabbitMqPublisherParameters parameters,
    ISerializer serializer) : IMessagePublisher
{
    
    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
    {
        var channel = await channelFactory.GetChannel();
        ArgumentNullException.ThrowIfNull(channel);
        
        await channel.ExchangeDeclareAsync(
            exchange: parameters.ExchangeName,
            type: parameters.ExchangeType,
            durable: parameters.Durable,
            cancellationToken: cancellationToken);
        
        var basicProperties = new BasicProperties();
        var body = serializer.Serialize(message);
        
        await channel.BasicPublishAsync(
            exchange: parameters.ExchangeName,   
            routingKey: parameters.RoutingKey,   
            mandatory: true,       
            basicProperties,
            body,
            cancellationToken: cancellationToken);
    }
}
