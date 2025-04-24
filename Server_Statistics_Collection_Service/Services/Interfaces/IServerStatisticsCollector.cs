namespace Server_Statistics_Collection_Service.Services.Interfaces;

public interface IServerStatisticsCollector
{
    public double GetMemoryUsage();
    public double GetCpuUsage();
    public double GetAvailableMemory();
    public DateTime GetTimestamp();
}
