namespace Domain.Configurations;

public class RabbitMqConfig
{
    public required string HostName { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
    public required string Exchange { get; init; }
    public required string QueueName { get; init; }
    public required string RoutingKey { get; init; }
}
