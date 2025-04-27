using AlertNotificationsConsumer.Services;
using AlertNotificationsConsumer.Services.Interfaces;

namespace AlertNotificationsConsumer;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var sig = "http://localhost:5199/alertHub";
        ISignalRAlertConsumerService consumerService = new SignalRAlertConsumerService(sig);
        await consumerService.ConsumeAlertsAsync();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }
}