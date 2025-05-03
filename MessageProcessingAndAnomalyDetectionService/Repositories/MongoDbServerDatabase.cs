using MessageProcessingAndAnomalyDetectionService.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class MongoDbServerDatabase (IOptions<MongoDbConfig> options)
{
    private readonly MongoDbConfig _mongoDbConfig = options.Value; 

    public IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(_mongoDbConfig.ConnectionString);
        var database = client.GetDatabase(_mongoDbConfig.DatabaseName);
        
        return database;
    }
}
