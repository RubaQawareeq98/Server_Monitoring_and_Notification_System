using MessageProcessingAndAnomalyDetectionService.AnomalyDetection.Interfaces;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;

namespace MessageProcessingAndAnomalyDetectionService.AnomalyDetection;

public class AnomalyDetector (IServerStatisticsRepository repository, IList<IAnomalyChecker> checkers) : IAnomalyDetector
{
    private ServerStatisticsResponse? _previousSeverStatistics;
    
    public async Task<List<Alert>> GetAlerts(ServerStatisticsResponse serverStatistics)
    {
        ArgumentNullException.ThrowIfNull(serverStatistics);

        _previousSeverStatistics = await repository.GetPreviousStatisticAsync(serverStatistics.ServerIdentifier);

        return (from checker in checkers let isAnomaly = checker.IsAnomalyDetected(serverStatistics, _previousSeverStatistics) where isAnomaly select checker.GetAlert()).ToList();
    }
}
