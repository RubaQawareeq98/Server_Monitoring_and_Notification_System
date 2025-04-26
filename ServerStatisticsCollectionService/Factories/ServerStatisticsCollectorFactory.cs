using ServerStatisticsCollectionService.Factories.Interfaces;
using ServerStatisticsCollectionService.Services.Interfaces;
using ServerStatisticsCollectionService.Strategies;

namespace ServerStatisticsCollectionService.Factories;

public class ServerStatisticsCollectorFactory : IServerStatisticsCollectorFactory
{
    public IServerStatisticsCollector GetServerStatisticsCollector(PlatformID platform)
    {
        return platform switch
        {
            PlatformID.Win32S => new WindowsServerStatisticsCollector(),
            _ => throw new NotSupportedException($"Platform {nameof(platform)} is not supported")
        };
    }
}
