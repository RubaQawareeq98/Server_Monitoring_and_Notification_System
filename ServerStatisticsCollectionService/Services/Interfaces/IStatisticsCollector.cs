
using Domain;

namespace ServerStatisticsCollectionService.Services.Interfaces;

public interface IStatisticsCollector
{
    ServerStatistics CollectServerStatistics();    
}