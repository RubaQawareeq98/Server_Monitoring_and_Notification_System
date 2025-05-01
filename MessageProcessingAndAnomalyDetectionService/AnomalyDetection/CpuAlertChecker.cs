using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class CpuAlertChecker (IServerStatisticsRepository repository, IOptions<AnomalyDetectionConfig> options) : IAlertChecker
{
    private readonly AnomalyDetectionConfig _config = options.Value; 
    
    public async Task<bool> IsAlertDetected(ServerStatisticsResponse serverStatistics)
    {
        var previousSeverStatistics = await repository.GetPreviousStatisticAsync(serverStatistics.ServerIdentifier);
        if (previousSeverStatistics is null)
        {
            return false;
        }
        
        return serverStatistics.CpuUsage >
               previousSeverStatistics.CpuUsage * (1 + _config.CpuUsageAnomalyThresholdPercentage);
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