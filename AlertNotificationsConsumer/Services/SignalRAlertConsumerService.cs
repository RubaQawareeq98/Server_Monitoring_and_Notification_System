using AlertNotificationsConsumer.Services.Interfaces;
using Domain.Configurations;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlertNotificationsConsumer.Services;

public class SignalRAlertConsumerService(IOptions<SignalRConfig> options, ILogger<SignalRAlertConsumerService> logger) : ISignalRAlertConsumerService
{
    public async Task ConsumeAlertsAsync()
    {
        var signalRUrl = options.Value.SignalRUrl;
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(signalRUrl)
            .WithAutomaticReconnect()
            .Build();

        hubConnection.On<string, string>("ReceiveAlert", (alertType, message) =>
        {
            logger.LogInformation("Received Alert: {AlertType} - {Message}", alertType, message);
        });
        
        await hubConnection.StartAsync();
        logger.LogInformation("Connected to SignalR Hub.");
    }
}