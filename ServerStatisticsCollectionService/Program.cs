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
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var service = new ServiceCollection();
            
            service.AddServices(configuration);
            var provider = service.BuildServiceProvider();
        
            var publisher = provider.GetService<IServerStatisticsPublisher>() ?? throw new InvalidOperationException();
        
            await publisher.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
