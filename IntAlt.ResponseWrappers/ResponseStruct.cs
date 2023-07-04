namespace IntAlt.ResponseWrappers;

/// <summary>
/// A basic response with a result object / value.
/// </summary>
/// <typeparam name="T">Result struct type</typeparam>
public class ResponseStruct<T> : Response where T: struct
{
    /// <summary>
    /// Default constructor. May be required for deserialization
    /// </summary>
    public ResponseStruct()
    {
        this.Result = null;
    }

    /// <summary>
    /// Error constructor. No result value.
    /// </summary>
    /// <param name="code">Error code.</param>
    public ResponseStruct(int code) : base(code)
    {
        this.Result = null;
    }

    /// <summary>
    /// Success constructor.
    /// </summary>
    /// <param name="result">Result value</param>
    /// <param name="responseCode">Response code - default = "Success" / 0</param>
    public ResponseStruct(T? result, int responseCode = 0) : base(responseCode)
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
        return (obj is ResponseStruct<T> response) &&
               base.Equals(obj) &&
               (this.Code == response.Code) &&
               EqualityComparer<T?>.Default.Equals(Result, response.Result);
    }

    /// <inheritdoc />
    public static bool operator ==(ResponseStruct<T>? left, ResponseStruct<T>? right)
    {
        return EqualityComparer<ResponseStruct<T>>.Default.Equals(left, right);
    }

    /// <inheritdoc />
    public static bool operator !=(ResponseStruct<T>? left, ResponseStruct<T>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc />
    public override int GetHashCode() => base.GetHashCode();
}