using System.Text;

namespace RabbitMqLibrary.Serializers;

public class JsonSerializer : ISerializer
{
    public byte[] Serialize<TMessage>(TMessage message)
    {
        var serializedMessage = System.Text.Json.JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);
        return body;
    }
}
