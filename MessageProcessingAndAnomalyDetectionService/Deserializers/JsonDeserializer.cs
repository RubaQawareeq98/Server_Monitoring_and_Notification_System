using System.Text.Json;
using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Deserializers;

public class JsonDeserializer : IDeserializer
{
    public SeverStatisticsResponse Deserialize(string data)
    {
        return JsonSerializer.Deserialize<SeverStatisticsResponse>(data) ?? throw new InvalidOperationException();
    }
}
