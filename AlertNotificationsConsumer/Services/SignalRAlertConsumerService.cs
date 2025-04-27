using AlertNotificationsConsumer.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace AlertNotificationsConsumer.Services;

public class SignalRAlertConsumerService(string signalRUrl) : ISignalRAlertConsumerService
{
    public async Task ConsumeAlertsAsync()
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(signalRUrl)
            .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string, string>("ReceiveAlert", (title, message) =>
        {
            Console.WriteLine($"Received Alert: {title} - {message}");
        });
        
        await hubConnection.StartAsync();
        Console.WriteLine("Connected to SignalR Hub.");
    }
}