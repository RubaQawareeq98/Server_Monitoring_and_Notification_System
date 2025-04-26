using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Factories.Interfaces;

public interface IServerStatisticsCollectorFactory
{
    IServerStatisticsCollector GetServerStatisticsCollector(PlatformID platform);
}
