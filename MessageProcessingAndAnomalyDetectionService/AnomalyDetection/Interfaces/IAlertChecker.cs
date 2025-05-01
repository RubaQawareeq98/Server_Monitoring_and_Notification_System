using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;

public interface IAlertChecker
{
    Task<bool> IsAlertDetected(ServerStatisticsResponse serverStatistics);
    Alert GetAlert();
}