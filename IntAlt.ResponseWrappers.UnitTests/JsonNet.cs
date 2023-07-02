using Newtonsoft.Json;

namespace IntAlt.ResponseWrappers.UnitTests;

/// <summary>
/// Tests using the Newtonsoft serializer / deserializer
/// </summary>
[TestClass()]
internal class JsonNet : ISerializerDeserializer
{
    public string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
