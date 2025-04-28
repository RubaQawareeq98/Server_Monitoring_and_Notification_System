using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;

public interface IAnomalyChecker
{
    bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics);
    Alert GetAlert();
}