using Server_Statistics_Collection_Service.Models;

namespace Server_Statistics_Collection_Service.Services.Interfaces;

public interface IStatisticsCollector
{
    ServerStatistics CollectServerStatistics();    
}