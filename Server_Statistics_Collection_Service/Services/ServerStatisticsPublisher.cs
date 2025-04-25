using Server_Statistics_Collection_Service.Configurations;
using Server_Statistics_Collection_Service.Models;
using Server_Statistics_Collection_Service.Serializers;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Services;

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
