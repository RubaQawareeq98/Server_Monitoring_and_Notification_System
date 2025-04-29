using AlertNotificationsConsumer.Services;
using AlertNotificationsConsumer.Services.Interfaces;
using Domain.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace AlertNotificationsConsumer;

abstract class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();

        services.Configure<SignalRConfig>(configuration.GetSection("SignalRConfig"));
        services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Information));

        
        services.AddSingleton<ISignalRAlertConsumerService, SignalRAlertConsumerService>();
        
        var serviceProvider = services.BuildServiceProvider();
        try
        {
            var consumerService = serviceProvider.GetService<ISignalRAlertConsumerService>();
            if (consumerService != null) await consumerService.ConsumeAlertsAsync();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Press any key to exit...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        

    }
}
