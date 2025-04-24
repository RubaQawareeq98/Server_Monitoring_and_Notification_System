namespace Server_Statistics_Collection_Service.Configurations;

public class ServerStatisticsConfig
{
    public int SamplingIntervalSeconds { get; set; }
    public required string ServerIdentifier { get; set; }
}
