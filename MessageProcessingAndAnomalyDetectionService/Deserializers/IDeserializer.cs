using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Deserializers;

public interface IDeserializer
{
    ServerStatisticsResponse Deserialize(string data);
}
