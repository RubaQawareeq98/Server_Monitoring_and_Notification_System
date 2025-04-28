using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;
using Microsoft.Extensions.Options;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class MemoryHighUsageChecker (IOptions<AnomalyDetectionConfig> options): IAnomalyChecker
{
    private readonly AnomalyDetectionConfig _config = options.Value; 

    public bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics)
    {
        return serverStatistics.MemoryUsage / (serverStatistics.MemoryUsage + serverStatistics.AvailableMemory) >
               _config.MemoryUsageThresholdPercentage;
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