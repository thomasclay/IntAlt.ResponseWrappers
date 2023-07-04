namespace IntAlt.ResponseWrappers;

/// <summary>
/// A response struct with detailed error information
/// </summary>
public class DetailedResponseStruct<T> : ResponseStruct<T> where T: struct
{
    /// <summary>
    /// Default constructor - useful for deserialization
    /// </summary>
    public DetailedResponseStruct()
    { }

    /// <summary>
    /// Error constructor
    /// </summary>
    /// <param name="responseCode">Response code - should be negative</param>
    /// <param name="error">Error details</param>
    public DetailedResponseStruct(int responseCode, Error error) : base(responseCode)
    {
        this.Error = error;
    }

    /// <summary>
    /// Error constructor
    /// </summary>
    /// <param name="messageCode">message code to generate error</param>
    public DetailedResponseStruct(MessageCode messageCode) : base(messageCode.Code)
    {
        this.Error = new Error(messageCode);
    }

    /// <summary>
    /// Success constructor
    /// </summary>
    /// <param name="result">The result</param>
    /// <param name="responseCode">Success response code - should be positive</param>
    /// <param name="error">Detailed error information</param>
    public DetailedResponseStruct(T result, int responseCode = 0, Error? error = null) : base(result, responseCode)
    {
        this.Error = error;
    }

    /// <summary>
    /// Detailed error information
    /// </summary>
    public Error? Error { get; set; }
}
