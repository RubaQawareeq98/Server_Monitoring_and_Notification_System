using Server_Statistics_Collection_Service.Strategies.Interfaces;

namespace Server_Statistics_Collection_Service.Factories.Interfaces;

public interface IServerStatisticsCollectorFactory
{
    IServerStatisticsCollector _serverStatisticsCollector(PlatformID platform);
}
