using System.Text;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Extensions;
using MessageProcessingAndAnomalyDetectionService.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageProcessingAndAnomalyDetectionService.Services;

public class RabbitMqMessageConsumer (RabbitMqConfig rabbitMqConfig) : IRabbitMqMessageConsumer
{
    private readonly IConnectionFactory _factory = new ConnectionFactory { HostName = rabbitMqConfig.HostName, UserName = rabbitMqConfig.UserName, Password = rabbitMqConfig.Password};

    public async Task ConsumeMessage()
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.DeclareAsync(rabbitMqConfig);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += (_, ea) =>
        {
                var body = ea.Body.ToArray();
                var msg = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] Received: {msg}");
                Console.WriteLine($"args: {ea.RoutingKey}.");
                return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(
            queue: rabbitMqConfig.QueueName,
            autoAck: false,
            consumer: consumer);
    }
}
