using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;
using Microsoft.Extensions.Options;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class CpuHighUsageAlert (IOptions<AnomalyDetectionConfig> options) : IAlertChecker
{
    private readonly AnomalyDetectionConfig _config = options.Value; 

    public async Task<bool> IsAlertDetected(ServerStatisticsResponse serverStatistics)
    {
        return await Task.FromResult(serverStatistics.CpuUsage > _config.CpuUsageThresholdPercentage);
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