using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;

public interface IServerStatisticsRepository
{
    Task InsertServerStatisticsAsync(SeverStatisticsResponse serverStatistics);
}
