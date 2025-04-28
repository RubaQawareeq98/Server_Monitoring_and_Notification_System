namespace ServerStatisticsCollectionService.Services.Interfaces;

public interface IMessagePublisher
{
    Task InitializeAsync();
    Task Publish(string message);
}
