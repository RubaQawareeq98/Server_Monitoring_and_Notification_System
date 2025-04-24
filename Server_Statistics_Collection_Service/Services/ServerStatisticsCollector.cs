using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Services;

public class ServerStatisticsCollector : IServerStatisticsCollector
{
    public double GetMemoryUsage()
    {
        throw new NotImplementedException();
    }

    public double GetCpuUsage()
    {
        throw new NotImplementedException();
    }

    public double GetAvailableMemory()
    {
        throw new NotImplementedException();
    }

    public DateTime GetTimestamp()
    {
        throw new NotImplementedException();
    }
}
