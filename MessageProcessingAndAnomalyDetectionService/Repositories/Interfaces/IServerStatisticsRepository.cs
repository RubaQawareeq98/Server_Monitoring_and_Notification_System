using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;

public interface IServerStatisticsRepository
{
    Task InsertServerStatisticsAsync(ServerStatisticsResponse serverStatistics);
    Task<ServerStatisticsResponse?> GetPreviousStatisticAsync(string serverIdentifier);
}
