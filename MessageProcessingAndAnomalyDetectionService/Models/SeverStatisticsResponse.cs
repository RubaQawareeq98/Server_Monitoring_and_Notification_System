using Domain;

namespace MessageProcessingAndAnomalyDetectionService.Models;

public class SeverStatisticsResponse : ServerStatistics
{
    public required string ServerIdentifier { get; set; }
}
