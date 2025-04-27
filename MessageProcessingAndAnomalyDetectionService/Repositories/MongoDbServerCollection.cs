using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MessageProcessingAndAnomalyDetectionService.Repositories;

public class MongodbServerCollection (IConfigurationRoot configuration)
{
    public IMongoDatabase GetCollection()
    {
        
        var connectionString = configuration.GetSection("MongoDbConfig").GetSection("ConnectionString").Get<string>();
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Inventory_Management_System");
        
        return database;
    }
}
