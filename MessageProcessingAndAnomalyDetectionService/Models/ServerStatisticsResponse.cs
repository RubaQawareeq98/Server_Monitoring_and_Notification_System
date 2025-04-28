using Domain.Models;

namespace MessageProcessingAndAnomalyDetectionService.Models;

public class ServerStatisticsResponse : ServerStatistics
{
    public string ServerIdentifier { get; set; } = string.Empty;
}
