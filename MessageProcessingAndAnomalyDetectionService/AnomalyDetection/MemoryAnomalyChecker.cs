using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;
using Microsoft.Extensions.Options;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class MemoryAnomalyChecker (IOptions<AnomalyDetectionConfig> options) : IAnomalyChecker
{
    private readonly AnomalyDetectionConfig _config = options.Value; 

    public bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics)
    {
        if (previousSeverStatistics is null)
        {
            return false;
        }
        return serverStatistics.MemoryUsage > previousSeverStatistics.MemoryUsage * (1 + _config.MemoryUsageAnomalyThresholdPercentage);
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
