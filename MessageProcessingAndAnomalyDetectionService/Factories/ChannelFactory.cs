using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Factories.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MessageProcessingAndAnomalyDetectionService.Factories;

public class ChannelFactory (IOptions<RabbitMqConfig> options) : IChannelFactory
{
    private readonly RabbitMqConfig _rabbitMqConfig = options.Value;

    public async Task<IChannel> GetChannel()
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.HostName,
            UserName = _rabbitMqConfig.UserName,
            Password = _rabbitMqConfig.Password
        };
        
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        await channel.ExchangeDeclareAsync(
            exchange: _rabbitMqConfig.Exchange,
            type: ExchangeType.Topic,
            durable: true
        );
        
        await channel.QueueDeclareAsync(
            queue: _rabbitMqConfig.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        await channel.QueueBindAsync(
            queue: _rabbitMqConfig.QueueName,
            exchange: _rabbitMqConfig.Exchange,
            routingKey: "ServerStatistics.*");
        
        return channel;
    }
}
