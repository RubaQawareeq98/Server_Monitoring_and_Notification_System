namespace Server_Statistics_Collection_Service.Strategies.Interfaces;

public interface IServerStatisticsCollector
{
    double GetAvailableMemory();
    double GetMemoryUsage();
    double GetCpuUsage();
    DateTime GetTimestamp();
}
