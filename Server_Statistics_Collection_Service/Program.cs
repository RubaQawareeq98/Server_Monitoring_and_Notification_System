using Microsoft.Extensions.Configuration;
using Server_Statistics_Collection_Service.Configurations;
using Server_Statistics_Collection_Service.Services;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var settings = configuration.GetSection("ServerStatisticsConfig").Get<ServerStatisticsConfig>();
        IServerStatisticsCollector collector = new ServerStatisticsCollector();
        var availableMemory = collector.GetAvailableMemory();
        var usedMemory = collector.GetMemoryUsage();
        var usageCpu = collector.GetCpuUsage();
        var time = collector.GetTimestamp();
        Console.WriteLine($"Available Memory: {availableMemory}");
        Console.WriteLine($"Used Memory: {usedMemory}");
        Console.WriteLine($"Usage Cpu: {usageCpu}");
        Console.WriteLine($"Time: {time}");
        
    }
}