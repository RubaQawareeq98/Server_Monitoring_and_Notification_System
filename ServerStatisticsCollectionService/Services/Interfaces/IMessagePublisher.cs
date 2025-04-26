namespace ServerStatisticsCollectionService.Services.Interfaces;

public interface IMessagePublisher
{
    Task Publish(string message);
}
