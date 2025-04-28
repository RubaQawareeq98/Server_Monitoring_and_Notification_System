using MessageProcessingAndAnomalyDetectionService.Configurations;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interfaces;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class ServerStatisticsRepository (IMongoDatabase database, MongoDbConfig mongoDbConfig) : IServerStatisticsRepository
{
    private readonly IMongoCollection<ServerStatisticsResponse> _statisticsCollection = database.GetCollection<ServerStatisticsResponse>(mongoDbConfig.CollectionName);
    
    public async Task InsertServerStatisticsAsync(ServerStatisticsResponse serverStatistics)
    {
        await _statisticsCollection.InsertOneAsync(serverStatistics);
    }

    public async Task<ServerStatisticsResponse?> GetPreviousStatisticAsync(string serverIdentifier)
    {
        var serverStatistics = await _statisticsCollection
            .Find(s => s.ServerIdentifier == serverIdentifier)
            .Project<ServerStatisticsResponse>(Builders<ServerStatisticsResponse>.Projection.Exclude("_id"))
            .SortByDescending(s => s.Timestamp)
            .FirstOrDefaultAsync();
        return serverStatistics;
    }
}
