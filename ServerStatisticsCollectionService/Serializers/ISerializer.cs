namespace ServerStatisticsCollectionService.Serializers;

public interface ISerializer<in T>
{
    string Serialize(T obj);
}