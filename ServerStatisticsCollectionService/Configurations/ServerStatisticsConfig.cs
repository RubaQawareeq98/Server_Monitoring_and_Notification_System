namespace ServerStatisticsCollectionService.Configurations;

public class ServerStatisticsConfig
{
    public int SamplingIntervalSeconds { get; init; }
    public required string ServerIdentifier { get; init; }
}
