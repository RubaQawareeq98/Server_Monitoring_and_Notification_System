using Server_Statistics_Collection_Service.Models;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Services;

public class StatisticsCollector (IServerStatisticsCollector collector) : IStatisticsCollector
{
    public ServerStatistics CollectServerStatistics()
    {
        return new ServerStatistics
        {
            AvailableMemory = collector.GetAvailableMemory(),
            CpuUsage = collector.GetCpuUsage(),
            MemoryUsage = collector.GetMemoryUsage(),
            Timestamp = collector.GetTimestamp()
        };
    }
}
