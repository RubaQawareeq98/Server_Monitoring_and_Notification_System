using Domain.Models;

namespace MessageProcessingAndAnomalyDetectionService.Models;

public class SeverStatisticsResponse : ServerStatistics
{
    public string ServerIdentifier { get; set; } = string.Empty;
}
