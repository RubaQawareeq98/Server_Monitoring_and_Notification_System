using MessageProcessingAndAnomalyDetectionService.Configurations;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class MongoDbServerDatabase (MongoDbConfig mongoDbConfig)
{
    public IMongoDatabase GetDatabase()
    {
        
        var connectionString = mongoDbConfig.ConnectionString;
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(mongoDbConfig.DatabaseName);
        
        return database;
    }
}
