using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;

public interface IAnomalyDetector
{
    Task<List<Alert>> GetAlerts(SeverStatisticsResponse serverStatistics);
}
