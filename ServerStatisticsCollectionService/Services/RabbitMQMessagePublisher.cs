using System.Text;
using Domain.Configurations;
using RabbitMQ.Client;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Factories.Interfaces;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Services;

public class RabbitMqMessagePublisher(
    IChannelFactory channelFactory,
    RabbitMqConfig rabbitMqConfig,
    ServerStatisticsConfig statisticsConfig) : IMessagePublisher
{
    private readonly string _topic = $"ServerStatistics.{statisticsConfig.ServerIdentifier}";
    private IChannel? _channel;

    public async Task InitializeAsync()
    {
        Console.WriteLine("Initializing RabbitMQ message publisher");
        Console.WriteLine(rabbitMqConfig.Exchange);
        _channel = await channelFactory.GetChannel();
        Console.WriteLine("Creating RabbitMQ Channel");
        Console.WriteLine(rabbitMqConfig.HostName);
    }
    
    public async Task Publish(string message)
    {
        ArgumentNullException.ThrowIfNull(_channel);
        
        var basicProperties = new BasicProperties();
        var body = Encoding.UTF8.GetBytes(message);
        
        await _channel.BasicPublishAsync(
            exchange: rabbitMqConfig.Exchange,   
            routingKey: _topic,   
            mandatory: true,       
            basicProperties,
            body
        );
    }
}
