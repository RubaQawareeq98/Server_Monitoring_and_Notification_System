using System.Text.Json;

namespace RabbitMqLibrary.Deserializers;

public class JsonDeserializer : IDeserializer
{
    public TMessage Deserialize<TMessage>(byte[] message)
    {
        return JsonSerializer.Deserialize<TMessage>(message) ?? throw new InvalidOperationException();

    }
}