using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class ServerStatisticsRepository (IMongoDatabase database) : IServerStatisticsRepository
{
    private readonly IMongoCollection<SeverStatisticsResponse> _statisticsCollection = database.GetCollection<SeverStatisticsResponse>("ServerStatistics");
    
    public async Task InsertServerStatistics(SeverStatisticsResponse serverStatistics)
    {
        await _statisticsCollection.InsertOneAsync(serverStatistics);
    }
}
