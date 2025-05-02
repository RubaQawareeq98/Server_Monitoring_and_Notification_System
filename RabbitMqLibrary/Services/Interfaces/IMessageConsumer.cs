namespace RabbitMqLibrary.Services.Interfaces;

public interface IMessageConsumer
{
    Task ConsumeAsync<TMessage>(Func<TMessage, Task> messageHandler);
}
