namespace ServerStatisticsCollectionService.Configurations;

public class RabbitMqConfig
{
    public required string HostName { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}
