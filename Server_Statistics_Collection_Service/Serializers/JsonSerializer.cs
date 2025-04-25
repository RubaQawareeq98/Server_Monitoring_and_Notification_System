using System.Text.Json;

namespace Server_Statistics_Collection_Service.Serializers;

public class JsonSerializer<T> : ISerializer<T>
{
    public string Serialize(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}
