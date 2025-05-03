namespace ServerStatisticsCollectionService.Services.Interfaces;

public interface IServerStatisticsCollector
{
    double GetAvailableMemory();
    double GetMemoryUsage();
    double GetCpuUsage();
    DateTime GetTimestamp();
}
