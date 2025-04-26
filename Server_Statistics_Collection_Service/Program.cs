using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server_Statistics_Collection_Service.Extensions;
using Server_Statistics_Collection_Service.Services.Interfaces;

namespace Server_Statistics_Collection_Service;

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
        
            var publisher = provider.GetService<IServerStatisticsPublisher>();
        
            await publisher?.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
