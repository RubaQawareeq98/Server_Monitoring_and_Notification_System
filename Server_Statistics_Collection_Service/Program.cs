using Microsoft.Extensions.Configuration;
using Server_Statistics_Collection_Service.Configurations;

namespace Server_Statistics_Collection_Service;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var settings = configuration.GetSection("ServerStatisticsConfig").Get<ServerStatisticsConfig>();
    }
}