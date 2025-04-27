using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Models.Enums;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class AnomalyDetector (AnomalyDetectionConfig config) : IAnomalyDetector
{
    private double _previousMemoryUsage = -1;
    private double _previousCpuUsage = -1;
    
    public List<Alert> GetAlerts(SeverStatisticsResponse serverStatistics)
    {
        ArgumentNullException.ThrowIfNull(serverStatistics);

        if (_previousCpuUsage == -1 || _previousMemoryUsage == -1)
        {
            _previousCpuUsage = serverStatistics.CpuUsage;
            _previousMemoryUsage = serverStatistics.MemoryUsage;
            return [];
        }
        
        var alerts = new List<Alert>();

        if (serverStatistics.MemoryUsage > _previousMemoryUsage * (1 + config.MemoryUsageAnomalyThresholdPercentage))
        {
            alerts.Add(
                new Alert
                {
                    AlertType = AlertType.AnomalyAlert,
                    Message = "Memory Usage Anomaly Alert."
                });
        }
        else if (serverStatistics.CpuUsage > _previousCpuUsage * (1 + config.CpuUsageAnomalyThresholdPercentage))
        {
            alerts.Add(
                new Alert
                {
                    AlertType = AlertType.AnomalyAlert,
                    Message = "Cpu Usage Anomaly Alert."
                });
        }
        else if (serverStatistics.MemoryUsage / (serverStatistics.MemoryUsage + serverStatistics.AvailableMemory) >
                 config.MemoryUsageThresholdPercentage)
        {
            alerts.Add(
                new Alert
                {
                    AlertType = AlertType.HighUsageAlert,
                    Message = "Memory High Usage Alert."
                });
        }
        else if (serverStatistics.CpuUsage > config.CpuUsageThresholdPercentage)
        {
            alerts.Add(
                new Alert
                {
                    AlertType = AlertType.HighUsageAlert,
                    Message = "Cpu High Usage Alert."
                });
        }
        
        _previousCpuUsage = serverStatistics.CpuUsage;
        _previousMemoryUsage = serverStatistics.MemoryUsage;

        return alerts;
    }
}