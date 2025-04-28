using Domain.Configurations;
using RabbitMQ.Client;
using ServerStatisticsCollectionService.Factories.Interfaces;

namespace ServerStatisticsCollectionService.Factories;

public class ChannelFactory (RabbitMqConfig rabbitMqConfig) : IChannelFactory
{
    public async Task<IChannel> GetChannel()
    {
        var factory = new ConnectionFactory
        {
            HostName = rabbitMqConfig.HostName,
            UserName = rabbitMqConfig.UserName,
            Password = rabbitMqConfig.Password
        };
        
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        await channel.ExchangeDeclareAsync(
            exchange: rabbitMqConfig.Exchange,
            type: ExchangeType.Topic,
            durable: true
        );
        
        return channel;
    }
}
