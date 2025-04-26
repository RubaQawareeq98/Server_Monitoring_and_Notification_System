using Domain;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Serializers;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Services;

public class ServerStatisticsPublisher (
    IStatisticsCollector collector,
    IMessagePublisher publisher,
    ISerializer<ServerStatistics> serializer,
    ServerStatisticsConfig config) : IServerStatisticsPublisher
{
    public async Task RunAsync()
    {
        for (var i = 0; i < 100; i++)
        {
            var statistics = collector.CollectServerStatistics();
            var message = serializer.Serialize(statistics);
            await publisher.Publish(message);

            await Task.Delay(config.SamplingIntervalSeconds * 1000);
        }
    }
}
