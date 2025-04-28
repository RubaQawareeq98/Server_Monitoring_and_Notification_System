using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Hubs.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace MessageProcessingAndAnomalyDetectionService.Hubs;

public class ServerStatisticsHub (SignalRConfig signalRConfig, ILogger logger) : IServerStatisticsHub
{
    public async Task SendAlertAsync(List<Alert> alerts)
    {
        if (alerts.Count == 0)
        {
            return;
        }
        logger.LogInformation("Sending alert to hub");
        
        var connection = new HubConnectionBuilder()
            .WithUrl(signalRConfig.SignalRUrl)
            .Build();

        await connection.StartAsync();
        
        foreach (var a in alerts)
        {
            await connection.InvokeCoreAsync("SendAlert", [nameof(a.AlertType), a.Message]);
        }
        await connection.DisposeAsync();
    }
}
