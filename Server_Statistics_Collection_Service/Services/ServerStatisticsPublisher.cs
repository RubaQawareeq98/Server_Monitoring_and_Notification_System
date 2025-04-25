using Server_Statistics_Collection_Service.Configurations;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Services;

public class ServerStatisticsPublisher (IStatisticsCollector collector, IMessagePublisher publisher, ServerStatisticsConfig config) : IServerStatisticsPublisher
{
    public async Task RunAsync()
    {
        var statistics = collector.CollectServerStatistics();
       // await publisher.Publish(statistics);

    }
}