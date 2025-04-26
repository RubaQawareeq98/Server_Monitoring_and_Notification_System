using System.Text;
using RabbitMQ.Client;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Services;

public class RabbitMqMessagePublisher(RabbitMqConfig config, ServerStatisticsConfig statisticsConfig) : IMessagePublisher
{
    private readonly IConnectionFactory _factory = new ConnectionFactory { HostName = config.HostName, UserName = config.UserName, Password = config.Password};
    private readonly string _topic = $"ServerStatistics.{statisticsConfig.ServerIdentifier}";

    public async Task Publish(string message)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var basicProperties = new BasicProperties();

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,   
            routingKey: _topic,   
            mandatory: true,       
            basicProperties: basicProperties,
            body: body
        );
    }
}
