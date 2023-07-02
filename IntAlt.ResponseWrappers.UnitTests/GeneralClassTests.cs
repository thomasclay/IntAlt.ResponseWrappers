namespace IntAlt.ResponseWrappers.UnitTests;

/// <summary>
/// Tests using the System.Text.Json serializer / deserializer
/// </summary>
[TestClass()]
public class GeneralClassTests
{

    [TestMethod()]
    [DataRow(typeof(SystemTextJson))]
    [DataRow(typeof(JsonNet))]
    public void BasicResponse(Type sd)
    {
        var serializer = CreateSerializer(sd);
        for (var ii = -499; ii <= 499; ii++)
        {
            var response = new Response(ii);
            var serialized = serializer.Serialize(response);
            var deserialized = serializer.Deserialize<Response>(serialized);
            Assert.AreEqual(response, deserialized);
            Assert.IsTrue(response == deserialized);
            Assert.IsFalse(response != deserialized);
        }
    }

    [TestMethod()]
    [DataRow(typeof(SystemTextJson))]
    [DataRow(typeof(JsonNet))]
    public void IntCollections(Type sd)
    {
        var serializer = CreateSerializer(sd);
        var response = new Response<int[]>(new int[] { 1, 2, 3, 4 });
        var serialized = serializer.Serialize(response);
        var deserializedArray = serializer.Deserialize<Response<int[]>>(serialized) ?? throw new Exception();

        Assert.AreEqual(response.Code, deserializedArray.Code);
        CollectionAssert.AreEquivalent(response.Result, deserializedArray.Result);

        // Deserialize as a list
        var deserializedList = serializer.Deserialize<Response<List<int>>>(serialized) ?? throw new Exception();

        Assert.AreEqual(response.Code, deserializedList.Code);
        CollectionAssert.AreEquivalent(response.Result, deserializedList.Result);
    }

    [TestMethod()]
    [DataRow(typeof(SystemTextJson))]
    [DataRow(typeof(JsonNet))]
    public void DetailedResponses(Type sd)
    {
        var serializer = CreateSerializer(sd);
        var response = new DetailedResponse<int[]>(-1, new Error(-1, "Error"));
        var serialized = serializer.Serialize(response);
        var deserialized = serializer.Deserialize<DetailedResponse<int[]>>(serialized) ?? throw new Exception();

        Assert.AreEqual(response.Code, deserialized.Code);
        Assert.AreEqual(response.Error, deserialized.Error);
        Assert.IsNull(response.Result);
        Assert.IsNull(deserialized.Result);

        response = new DetailedResponse<int[]>(Array.Empty<int>(), -1, new Error(-1, "Error"));
        serialized = serializer.Serialize(response);
        deserialized = serializer.Deserialize<DetailedResponse<int[]>>(serialized) ?? throw new Exception();

        Assert.AreEqual(response.Code, deserialized.Code);
        Assert.AreEqual(response.Error, deserialized.Error);
        Assert.IsNotNull(response.Result);
        Assert.IsFalse(response.Result.Any());
        Assert.IsNotNull(deserialized.Result);
        Assert.IsFalse(deserialized.Result.Any());
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
