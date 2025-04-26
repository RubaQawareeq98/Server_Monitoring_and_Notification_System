using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.Deserializers;

public interface IDeserializer
{
    SeverStatisticsResponse Deserialize(string data);
}
