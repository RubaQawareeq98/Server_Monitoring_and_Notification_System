using Microsoft.AspNetCore.SignalR;

namespace SignalRAlertNotificationService.AlertHub;

public class AlertNotificationHub : Hub<IAlertHubConsumer>
{
    public async Task SendAlert(string alertType, string message)
    {
        Console.WriteLine($"Sending message: {message}");
        await Clients.All.ReceiveAlert(alertType, message);
    }
}
