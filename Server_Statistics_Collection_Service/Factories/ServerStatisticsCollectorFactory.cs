using Server_Statistics_Collection_Service.Factories.Interfaces;
using Server_Statistics_Collection_Service.Strategies;
using Server_Statistics_Collection_Service.Strategies.Interfaces;

namespace Server_Statistics_Collection_Service.Factories;

public class ServerStatisticsCollectorFactory : IServerStatisticsCollectorFactory
{
    public IServerStatisticsCollector _serverStatisticsCollector(PlatformID platform)
    {
        return platform switch
        {
            PlatformID.Win32S => new WindowsServerStatisticsCollector(),
            _ => throw new NotSupportedException($"Platform {nameof(platform)} is not supported")
        };
    }
}
