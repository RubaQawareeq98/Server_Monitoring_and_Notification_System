namespace Server_Statistics_Collection_Service.Services.Interfaces;

public interface IServerStatisticsCollector
{
    double GetAvailableMemory();
    double GetMemoryUsage();
    double GetCpuUsage();
    DateTime GetTimestamp();
}
