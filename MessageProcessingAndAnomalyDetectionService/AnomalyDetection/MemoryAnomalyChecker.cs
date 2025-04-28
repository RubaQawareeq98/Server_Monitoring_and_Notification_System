using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class MemoryAnomalyChecker (AnomalyDetectionConfig config) : IAnomalyChecker
{
    public bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics)
    {
        if (previousSeverStatistics is null)
        {
            return false;
        }
        return serverStatistics.MemoryUsage > previousSeverStatistics.MemoryUsage * (1 + config.MemoryUsageAnomalyThresholdPercentage);
    }

    public Alert GetAlert()
    {
        return new Alert
        {
            AlertType = AlertType.AnomalyAlert,
            Message = "Memory Usage Anomaly Alert."
        };
    }
}
