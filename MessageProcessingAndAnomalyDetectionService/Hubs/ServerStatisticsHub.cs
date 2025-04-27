using MessageProcessingAndAnomalyDetectionService.Hubs.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace MessageProcessingAndAnomalyDetectionService.Hubs;

public class ServerStatisticsHub (string signalRUrl) : IServerStatisticsHub
{
    public async Task SendAlertAsync(List<Alert> alerts)
    {
        if (alerts.Count == 0)
        {
            return;
        }

        var connection = new HubConnectionBuilder()
            .WithUrl(signalRUrl)
            .Build();

        await connection.StartAsync();
        await connection.InvokeAsync("SendAlert", alerts);
        await connection.DisposeAsync();
    }
}
