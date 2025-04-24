namespace Server_Statistics_Collection_Service.Services.Interfaces;

public interface IServerStatisticsCollector
{
    public double GetAvailableMemory();
    public double GetMemoryUsage();
    public double GetCpuUsage();
    public DateTime GetTimestamp();
}
