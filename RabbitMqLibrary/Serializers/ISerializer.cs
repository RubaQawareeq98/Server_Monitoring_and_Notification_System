namespace RabbitMqLibrary.Serializers;

public interface ISerializer
{
    byte[] Serialize<TMessage>(TMessage message);
}
