using Microsoft.AspNetCore.SignalR;

namespace SignalRAlertNotificationService;

public class AlertNotificationHub : Hub
{
    public async Task SendAlert(string title, string message)
    {
        Console.WriteLine($"Sending message: {message}");
        await Clients.All.SendAsync("ReceiveAlert", title, message);
    }
}
