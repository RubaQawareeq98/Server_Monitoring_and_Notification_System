using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class AlertDetector(IEnumerable<IAlertChecker> checkers) : IAlertDetector
{
    public async Task<List<Alert>> GetAlerts(ServerStatisticsResponse serverStatistics)
    {
        ArgumentNullException.ThrowIfNull(serverStatistics);
        
        var alerts = new List<Alert>();
        foreach (var checker in checkers)
        {
            var isAlertDetected = await checker.IsAlertDetected(serverStatistics);
            if (isAlertDetected)
            {
                alerts.Add(checker.GetAlert());
            }
        }
        return alerts;
    }
}
