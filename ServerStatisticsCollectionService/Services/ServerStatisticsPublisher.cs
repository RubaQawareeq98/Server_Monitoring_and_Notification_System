using Domain;
using Domain.Models;
using ServerStatisticsCollectionService.Configurations;
using ServerStatisticsCollectionService.Serializers;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService.Services;

public class ServerStatisticsPublisher (
    IStatisticsCollector collector,
    IMessagePublisher publisher,
    ISerializer<ServerStatistics> serializer,
    ServerStatisticsConfig config) : IServerStatisticsPublisher
{
    public async Task RunAsync()
    {
        await publisher.InitializeAsync();
        
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
            Console.WriteLine("Press Q to exit");
            var statistics = collector.CollectServerStatistics();
            var message = serializer.Serialize(statistics);
            
            await publisher.Publish(message);

           await Task.Delay(config.SamplingIntervalSeconds * 1000);
        }
    }
}
