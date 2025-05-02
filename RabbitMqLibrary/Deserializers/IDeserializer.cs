namespace RabbitMqLibrary.Deserializers;

public interface IDeserializer
{
    TMessage Deserialize<TMessage>(byte[] message);
}
