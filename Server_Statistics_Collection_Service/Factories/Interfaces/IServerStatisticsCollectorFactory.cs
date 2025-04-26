using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Factories.Interfaces;

public interface IServerStatisticsCollectorFactory
{
    IServerStatisticsCollector GetServerStatisticsCollector(PlatformID platform);
}
