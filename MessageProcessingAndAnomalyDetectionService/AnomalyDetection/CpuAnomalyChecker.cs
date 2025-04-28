using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class CpuAnomalyChecker (AnomalyDetectionConfig config) : IAnomalyChecker
{
    public bool IsAnomalyDetected(ServerStatisticsResponse serverStatistics, ServerStatisticsResponse? previousSeverStatistics)
    {
        if (previousSeverStatistics is null)
        {
            return false;
        }
        
        return serverStatistics.CpuUsage >
               previousSeverStatistics.CpuUsage * (1 + config.CpuUsageAnomalyThresholdPercentage);
    }

    public Alert GetAlert()
    {
        return new Alert
        {
            AlertType = AlertType.AnomalyAlert,
            Message = "Cpu Usage Anomaly Alert."
        };
    }
}