using System.Text;
using Server_Statistics_Collection_Service.Services.Interfaces;
using RabbitMQ.Client;

namespace Server_Statistics_Collection_Service.Services;

public class RabbitMqMessagePublisher(string hostName, string serverIdentifier) : IMessagePublisher
{
    private readonly IConnectionFactory _factory = new ConnectionFactory { HostName = hostName };
    private readonly string _message = $"Server {serverIdentifier} is running on host {hostName}";

    public async Task Publish(string message)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var basicProperties = new BasicProperties();
        
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: this._message,
            mandatory: true,
            basicProperties: basicProperties,
            body: body);
    }
}
