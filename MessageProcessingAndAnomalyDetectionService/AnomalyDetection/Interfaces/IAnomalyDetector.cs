using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;

public interface IAnomalyDetector
{
    List<Alert> GetAlerts(SeverStatisticsResponse serverStatistics);
}
