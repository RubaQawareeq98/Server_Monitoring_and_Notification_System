using System.Text;
using Server_Statistics_Collection_Service.Services.Interfaces;
using RabbitMQ.Client;
using Server_Statistics_Collection_Service.Configurations;

namespace Server_Statistics_Collection_Service.Services;

public class RabbitMqMessagePublisher(RabbitMqConfig config, string serverIdentifier) : IMessagePublisher
{
    private readonly IConnectionFactory _factory = new ConnectionFactory { HostName = config.HostName, UserName = config.UserName, Password = config.Password};
    private readonly string _topic = $"Server {serverIdentifier} is running";

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
