using Microsoft.Extensions.Configuration;
using Server_Statistics_Collection_Service.Configurations;
using Server_Statistics_Collection_Service.Factories;
using Server_Statistics_Collection_Service.Factories.Interfaces;
using Server_Statistics_Collection_Service.Strategies;

namespace Server_Statistics_Collection_Service;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var settings = configuration.GetSection("ServerStatisticsConfig").Get<ServerStatisticsConfig>();
      
        IServerStatisticsCollectorFactory factory = new ServerStatisticsCollectorFactory();
        var statistic = factory._serverStatisticsCollector(PlatformID.Unix);
        Console.WriteLine("statistic");
         var availableMemory = statistic.GetAvailableMemory();
         var usedMemory = statistic.GetMemoryUsage();
         var usageCpu = statistic.GetCpuUsage();
         var time = statistic.GetTimestamp();
        Console.WriteLine($"Available Memory: {availableMemory}");
        Console.WriteLine($"Used Memory: {usedMemory}");
        Console.WriteLine($"Usage Cpu: {usageCpu}");
        Console.WriteLine($"Time: {time}");

    }
}