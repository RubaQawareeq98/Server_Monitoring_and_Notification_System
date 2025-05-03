using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Hubs.Interfaces;

public interface IServerStatisticsHub
{
    Task SendAlertAsync(List<Alert> alerts);
}