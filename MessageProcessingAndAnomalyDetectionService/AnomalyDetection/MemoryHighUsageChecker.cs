using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class MemoryHighUsageChecker (AnomalyDetectionConfig config): IAnomalyChecker
{
    public bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics)
    {
        return serverStatistics.MemoryUsage / (serverStatistics.MemoryUsage + serverStatistics.AvailableMemory) >
               config.MemoryUsageThresholdPercentage;
    }

    public Alert GetAlert()
    {
        return new Alert
        {
            AlertType = AlertType.HighUsageAlert,
            Message = "Memory High Usage Alert."
        };
    }
}