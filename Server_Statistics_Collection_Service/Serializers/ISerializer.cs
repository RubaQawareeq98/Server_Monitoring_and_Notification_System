namespace Server_Statistics_Collection_Service.Serializers;

public interface ISerializer<in T>
{
    string Serialize(T obj);
}