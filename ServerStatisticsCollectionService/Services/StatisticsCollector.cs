using Domain;
using Domain.Models;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Services;

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
