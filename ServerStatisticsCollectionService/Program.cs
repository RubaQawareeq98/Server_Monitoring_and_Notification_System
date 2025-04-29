using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerStatisticsCollectionService.Extensions;
using ServerStatisticsCollectionService.Services.Interfaces;

namespace ServerStatisticsCollectionService;

internal abstract class Program
{
    static async Task Main()
    {
        try
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            
            var tt = Environment.GetEnvironmentVariable("SERVER__IDENTIFIER");
            Console.WriteLine($"ServerStatisticsCollectionService {tt}");
            Console.WriteLine("Hello World!");
            Console.WriteLine("asdfg");

            var service = new ServiceCollection();
            
            service.AddServices(configuration);
            var provider = service.BuildServiceProvider();
            
            var publisher = provider.GetService<IServerStatisticsPublisher>() ?? throw new InvalidOperationException();
            Console.WriteLine($"Publisher: ");
            await publisher.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
