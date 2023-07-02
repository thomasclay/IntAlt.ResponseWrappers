namespace IntAlt.ResponseWrappers;

/// <summary>
/// A simple / basic response. Only has a single ResponseCode property.
/// </summary>
public class Response
{
    /// <summary>
    /// Default constructor. May be required for deserialization.
    /// </summary>
    public Response()
    { }

    /// <summary>
    /// Constructor with initializers.
    /// </summary>
    /// <param name="code">Response code</param>
    public Response(int code)
    {
        this.Code = code;
    }

    /// <summary>
    /// The returned response code.
    /// </summary>
    /// <remarks><para>Response codes should be stored in a master list (database table?) somewhere.
    /// Different modules / systems would have different response code ranges.</para>
    /// <para>Different modules will have differing ranges, where the range from -999 to 999 is for "common" results.</para>
    /// <para>0 indicates general success - everything went as intended. Each module / service will have their own ranges for
    /// custom success and error codes</para>
    /// </remarks>
    public int Code { get; set; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return (obj is Response response) &&
               (Code == response.Code);
    }

    /// <inheritdoc />
    public static bool operator ==(Response? left, Response? right)
    {
        return EqualityComparer<Response>.Default.Equals(left, right);
    }

    /// <inheritdoc />
    public static bool operator !=(Response? left, Response? right)
    {
        return !(left == right);
    }

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();
}

/// <summary>
/// A basic response with a result object / value.
/// </summary>
/// <typeparam name="T">Result type</typeparam>
public class Response<T> : Response where T: class
{
    /// <summary>
    /// Default constructor. May be required for deserialization
    /// </summary>
    public Response()
    {
        this.Result = null;
    }

    /// <summary>
    /// Error constructor. No result value.
    /// </summary>
    /// <param name="code">Error code.</param>
    public Response(int code) : base(code)
    {
        this.Result = null;
    }

    /// <summary>
    /// Success constructor.
    /// </summary>
    /// <param name="result">Result value</param>
    /// <param name="responseCode">Response code - default = "Success" / 0</param>
    public Response(T? result, int responseCode = 0) : base(responseCode)
    {
        Result = result;
    }

    /// <summary>
    /// The result.
    /// </summary>
    public T? Result { get; set; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return (obj is Response<T> response) &&
               base.Equals(obj) &&
               (this.Code == response.Code) &&
               EqualityComparer<T?>.Default.Equals(Result, response.Result);
    }

    /// <inheritdoc />
    public static bool operator ==(Response<T>? left, Response<T>? right)
    {
        return EqualityComparer<Response<T>>.Default.Equals(left, right);
    }

    /// <inheritdoc />
    public static bool operator !=(Response<T>? left, Response<T>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();
}