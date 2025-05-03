using System.Text.Json;
using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Deserializers;

public class JsonDeserializer : IDeserializer
{
    public ServerStatisticsResponse Deserialize(string data)
    {
        return JsonSerializer.Deserialize<ServerStatisticsResponse>(data) ?? throw new InvalidOperationException();
    }
}
