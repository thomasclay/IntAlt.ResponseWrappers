namespace IntAlt.ResponseWrappers.UnitTests;

/// <summary>
/// Tests using the System.Text.Json serializer / deserializer
/// </summary>
[TestClass()]
public class GeneralStructTests
{
    [TestMethod()]
    [DataRow(typeof(SystemTextJson))]
    [DataRow(typeof(JsonNet))]
    public void IntCollections(Type sd)
    {
        var serializer = CreateSerializer(sd);
        for (var ii = -1000; ii <= 1000; ii++)
        {
            var response = new ResponseStruct<int>(ii);
            var serialized = serializer.Serialize(response);
            var deserialized = serializer.Deserialize<ResponseStruct<int>>(serialized) ?? throw new Exception();

            Assert.AreEqual(response, deserialized);
            Assert.AreEqual(response.Code, deserialized.Code);
        }
    }

    [TestMethod()]
    [DataRow(typeof(SystemTextJson))]
    [DataRow(typeof(JsonNet))]
    public void DetailedResponseErrors(Type sd)
    {
        var serializer = CreateSerializer(sd);
        for (var ii = -1000; ii < 0; ii++)
        {
            var response = new DetailedResponseStruct<int>(ii, new Error(ii, $"Error {ii}"));
            var serialized = serializer.Serialize(response);
            var deserialized = serializer.Deserialize<ResponseStruct<int>>(serialized) ?? throw new Exception();

            Assert.AreEqual(response, deserialized);
            Assert.AreEqual(response.Code, deserialized.Code);
            Assert.AreEqual(response.Error?.Message, $"Error {ii}");
        }


    }

    private static ISerializerDeserializer CreateSerializer(Type sd)
    {
        if (!typeof(ISerializerDeserializer).IsAssignableFrom(sd))
        {
            throw new Exception($"Not an ISerializerDeserializer: {sd.FullName}");
        }

        var constructor = sd.GetConstructor(Type.EmptyTypes) ?? throw new Exception($"default constructor not found: {sd.FullName}");

        var serializer = (ISerializerDeserializer)constructor.Invoke(Type.EmptyTypes);
        return serializer;
    }
}
