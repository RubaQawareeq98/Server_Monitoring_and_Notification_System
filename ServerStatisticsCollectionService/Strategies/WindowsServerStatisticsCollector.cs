using System.Diagnostics;
using System.Runtime.InteropServices;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Strategies;

public class WindowsServerStatisticsCollector : IServerStatisticsCollector
{
    public double GetAvailableMemory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var lines = File.ReadAllLines("/proc/meminfo");
            foreach (var line in lines)
            {
                if (line.StartsWith("MemTotal:"))
                {
                    var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2 && double.TryParse(parts[1], out var kb))
                    {
                        return kb / 1024.0; // Convert to MB
                    }
                }
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Optionally fallback to PerformanceCounter (if needed for dev/testing)
            throw new PlatformNotSupportedException("Total memory fetch is not implemented for Windows yet.");
        }

        throw new PlatformNotSupportedException("Unsupported OS for getting total memory.");
    }

    
    public double GetMemoryUsage()
    {
        return 5.5;

        // double totalMemory = 0;
        //
        // foreach (var obj in totalSearcher.Get())
        // {
        //     var totalPhysicalMemory = (ulong)obj["TotalPhysicalMemory"];
        //     totalMemory += totalPhysicalMemory / (1024.0 * 1024.0);
        // }
        //
        // var freeMemory = GetAvailableMemory();
        // var usedMemory = totalMemory - freeMemory;
        // return usedMemory;
    }

    public double GetCpuUsage()
    {
        using var process = Process.GetCurrentProcess();
        return process.TotalProcessorTime.TotalMilliseconds / Environment.ProcessorCount;
        
        // var cpuUsageCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        // cpuUsageCounter.NextValue();
        // Thread.Sleep(1000);
        // return cpuUsageCounter.NextValue();
    }

    public DateTime GetTimestamp()
    {
        return DateTime.UtcNow;
    }
}
