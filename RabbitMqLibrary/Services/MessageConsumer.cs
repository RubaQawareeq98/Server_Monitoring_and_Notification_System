using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqLibrary.Deserializers;
using RabbitMqLibrary.Models;
using RabbitMqLibrary.Services.Interfaces;

namespace RabbitMqLibrary.Services;

public class MessageConsumer(
    IChannel channel,
    IDeserializer deserializer,
    RabbitMqConsumerParameters parameters) : IMessageConsumer
{
    
    public async Task ConsumeAsync<TMessage>(Func<TMessage, Task> messageHandler)
    {
        ArgumentNullException.ThrowIfNull(channel);
        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var message = deserializer.Deserialize<TMessage>(ea.Body.ToArray());
            await messageHandler(message);
        };
        
        await channel.BasicConsumeAsync(
            queue: parameters.QueueName,
            autoAck: true,
            consumer: consumer);
    }
}
