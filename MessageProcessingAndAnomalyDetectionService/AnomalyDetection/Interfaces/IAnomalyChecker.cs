using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;

public interface IAnomalyChecker
{
    bool IsAnomalyDetected(SeverStatisticsResponse serverStatistics, SeverStatisticsResponse? previousSeverStatistics);
    Alert GetAlert();
}