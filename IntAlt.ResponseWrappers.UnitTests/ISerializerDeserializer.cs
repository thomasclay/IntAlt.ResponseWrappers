namespace IntAlt.ResponseWrappers.UnitTests;

/// <summary>
/// Serializer / deserializer object interface
/// </summary>
internal interface ISerializerDeserializer
{
    T? Deserialize<T>(string json);
    string Serialize(object obj);
}
