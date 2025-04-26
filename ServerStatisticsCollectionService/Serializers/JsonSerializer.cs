using System.Text.Json;

namespace ServerStatisticsCollectionService.Serializers;

public class JsonSerializer<T> : ISerializer<T>
{
    public string Serialize(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}
