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
        var previousStats = new ServerStatisticsResponse 
        {
            ServerIdentifier = "server1",
            MemoryUsage = 1000, // 1GB
            AvailableMemory = 9000, // 9GB
            CpuUsage = 30 // 30%
        };

        return (from checker in checkers let isAnomaly = checker.IsAnomalyDetected(serverStatistics, previousStats) where isAnomaly select checker.GetAlert()).ToList();
    }
}
