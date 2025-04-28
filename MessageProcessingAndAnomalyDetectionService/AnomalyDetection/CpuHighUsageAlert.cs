using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;
using Microsoft.Extensions.Options;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class CpuHighUsageAlert (IOptions<AnomalyDetectionConfig> options) : IAnomalyChecker
{
    private readonly AnomalyDetectionConfig _config = options.Value; 

    public bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics)
    {
        return serverStatistics.CpuUsage > _config.CpuUsageThresholdPercentage;
    }

    public Alert GetAlert()
    {
        return new Alert
        {
            AlertType = AlertType.HighUsageAlert,
            Message = "Cpu High Usage Alert."
        };
    }
}