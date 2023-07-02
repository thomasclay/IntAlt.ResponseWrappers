using System.Text.Json;

namespace IntAlt.ResponseWrappers.UnitTests;

/// <summary>
/// Serializer / deserializer System.Text.Json
/// </summary>
internal class SystemTextJson : ISerializerDeserializer
{
    public string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}
