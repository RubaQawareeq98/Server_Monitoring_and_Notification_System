namespace Server_Statistics_Collection_Service.Services.Interfaces;

public interface IMessagePublisher
{
    Task Publish(string message);
}
