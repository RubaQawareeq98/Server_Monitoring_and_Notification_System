
using Domain;
using Domain.Models;

namespace ServerStatisticsCollectionService.Services.Interfaces;

public interface IStatisticsCollector
{
    ServerStatistics CollectServerStatistics();    
}