using System.Text;
using RabbitMQ.Client;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Services;

public class RabbitMqMessagePublisher(RabbitMqConfig rabbitMqConfig, ServerStatisticsConfig statisticsConfig) : IMessagePublisher
{
    private readonly IConnectionFactory _factory = new ConnectionFactory { HostName = rabbitMqConfig.HostName, UserName = rabbitMqConfig.UserName, Password = rabbitMqConfig.Password};
    private readonly string _topic = $"ServerStatistics.{statisticsConfig.ServerIdentifier}";

    public async Task Publish(string message)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var basicProperties = new BasicProperties();

        var body = Encoding.UTF8.GetBytes(message);
        
        await channel.ExchangeDeclareAsync(
            exchange: rabbitMqConfig.Exchange,
            type: ExchangeType.Topic,
            durable: true
        );
        
        await channel.BasicPublishAsync(
            exchange: rabbitMqConfig.Exchange,   
            routingKey: _topic,   
            mandatory: true,       
            basicProperties: basicProperties,
            body: body
        );
    }
}
