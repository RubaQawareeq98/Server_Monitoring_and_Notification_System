using RabbitMQ.Client;

namespace RabbitMqLibrary.Factories.Interfaces;

public interface IChannelFactory
{
    Task<IChannel?> GetChannel();
}