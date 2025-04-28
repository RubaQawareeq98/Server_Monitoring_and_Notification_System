using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class ServerStatisticsRepository (IMongoDatabase database, MongoDbConfig mongoDbConfig) : IServerStatisticsRepository
{
    private readonly IMongoCollection<SeverStatisticsResponse> _statisticsCollection = database.GetCollection<SeverStatisticsResponse>(mongoDbConfig.CollectionName);
    
    public async Task InsertServerStatisticsAsync(SeverStatisticsResponse serverStatistics)
    {
        await _statisticsCollection.InsertOneAsync(serverStatistics);
    }
}
