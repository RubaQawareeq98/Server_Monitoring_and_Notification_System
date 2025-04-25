using System.Diagnostics;
using System.Management;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service.Strategies;

public class WindowsServerStatisticsCollector : IServerStatisticsCollector
{
    public double GetAvailableMemory()
    {
        var availableMemoryCounter = new PerformanceCounter("Memory", "Available MBytes");
        
        return availableMemoryCounter.NextValue();
    }
    
    public double GetMemoryUsage()
    {
        var totalQuery = new ObjectQuery("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
        var totalSearcher = new ManagementObjectSearcher(totalQuery);
        double totalMemory = 0;
    
        foreach (var obj in totalSearcher.Get())
        {
            var totalPhysicalMemory = (ulong)obj["TotalPhysicalMemory"];
            totalMemory += totalPhysicalMemory / (1024.0 * 1024.0);
        }
        
        var freeMemory = GetAvailableMemory();
        var usedMemory = totalMemory - freeMemory;
        return usedMemory;
    }

    public double GetCpuUsage()
    {
        var cpuUsageCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        cpuUsageCounter.NextValue();
        Thread.Sleep(1000);
        return cpuUsageCounter.NextValue();
    }

    public DateTime GetTimestamp()
    {
        return DateTime.UtcNow;
    }
}
