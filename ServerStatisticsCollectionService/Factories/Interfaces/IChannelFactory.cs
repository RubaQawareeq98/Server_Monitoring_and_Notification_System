using RabbitMQ.Client;

namespace ServerStatisticsCollectionService.Factories.Interfaces;

public interface IChannelFactory
{
    Task<IChannel> GetChannel();
}
