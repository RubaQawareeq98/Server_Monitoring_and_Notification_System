using MessageProcessingAndAnomalyDetectionService.Configurations;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class MongoDbServerDatabase (MongoDbConfig mongoDbConfig)
{
    public IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(mongoDbConfig.ConnectionString);
        var database = client.GetDatabase(mongoDbConfig.DatabaseName);
        
        return database;
    }
}
