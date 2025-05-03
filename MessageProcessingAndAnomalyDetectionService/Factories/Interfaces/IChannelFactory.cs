using RabbitMQ.Client;

namespace MessageProcessingAndAnomalyDetectionService.Factories.Interfaces;

public interface IChannelFactory
{
    Task<IChannel> GetChannel();
}
