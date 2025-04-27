using MessageProcessingAndAnomalyDetectionService.Configurations;
using RabbitMQ.Client;

namespace MessageProcessingAndAnomalyDetectionService.Extensions;

public static class ChannelDeclaration
{
    public static async Task DeclareAsync(this IChannel channel, RabbitMqConfig rabbitMqConfig)
    {
        ArgumentNullException.ThrowIfNull(channel);
        
        await channel.ExchangeDeclareAsync(
            exchange: rabbitMqConfig.Exchange,
            type: ExchangeType.Topic,
            durable: true
        );
        
        await channel.QueueDeclareAsync(
            queue: rabbitMqConfig.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        await channel.QueueBindAsync(
            queue: rabbitMqConfig.QueueName,
            exchange: rabbitMqConfig.Exchange,
            routingKey: "ServerStatistics.*");
    }
}
