using ServerStatisticsCollectionService.Models;

namespace ServerStatisticsCollectionService.Services.Interfaces;

public interface IStatisticsCollector
{
    ServerStatistics CollectServerStatistics();    
}