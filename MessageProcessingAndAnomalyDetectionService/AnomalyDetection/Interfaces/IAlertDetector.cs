using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;

public interface IAlertDetector
{
    Task<List<Alert>> GetAlerts(ServerStatisticsResponse serverStatistics);
}
