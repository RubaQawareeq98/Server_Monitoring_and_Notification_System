using Domain.Configurations;
using MessageProcessingAndAnomalyDetectionService.Hubs.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MessageProcessingAndAnomalyDetectionService.Hubs;

public class ServerStatisticsHub (IOptions<SignalRConfig> options, ILogger<ServerStatisticsHub> logger) : IServerStatisticsHub
{
    private readonly SignalRConfig _signalRConfig = options.Value;
    public async Task SendAlertAsync(List<Alert> alerts)
    {
        if (alerts.Count == 0)
        {
            return;
        }
        logger.LogInformation("Sending alert to hub");
        
        var connection = new HubConnectionBuilder()
            .WithUrl(_signalRConfig.SignalRUrl)
            .Build();

        await connection.StartAsync();
        
        foreach (var a in alerts)
        {
            await connection.InvokeCoreAsync("SendAlert", [a.AlertType.ToString(), a.Message]);
        }
        await connection.DisposeAsync();
    }
}
