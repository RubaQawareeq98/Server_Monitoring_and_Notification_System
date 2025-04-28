using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class CpuHighUsageAlert (AnomalyDetectionConfig config) : IAnomalyChecker
{
    public bool IsAnomalyDetected(SeverStatisticsResponse serverStatistics, SeverStatisticsResponse? previousSeverStatistics)
    {
        return serverStatistics.CpuUsage > config.CpuUsageThresholdPercentage;
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