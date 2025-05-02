namespace RabbitMqLibrary.Models;

public class RabbitMqPublisherParameters
{
    public string ExchangeName { get; set; } = string.Empty;
    public string RoutingKey { get; set; } = string.Empty;
    public string ExchangeType { get; set; } = string.Empty;
    public bool Durable { get; set; }
}
