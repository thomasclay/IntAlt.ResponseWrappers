namespace IntAlt.ResponseWrappers;

/// <summary>
/// Error class for a detailed response. Typically used for delivering information to a UI.
/// </summary>
public class Error
{ 
    /// <summary>
    /// Default constructor. Useful for deserialization
    /// </summary>
    public Error()
    {
        this.Message = string.Empty;
    }

    /// <summary>
    /// Full parameter constructor
    /// </summary>
    /// <param name="responseCode">Error response code - should be a negative value</param>
    /// <param name="message">Error message</param>
    /// <param name="errors">Aggregate errors, if defined</param>
    public Error(int responseCode, string message, IEnumerable<Error>? errors = null)
    {
        this.ResponseCode = responseCode;
        this.Message = message;
        this.Errors = errors;
    }

    /// <summary>
    /// Aggregate error constructor
    /// </summary>
    /// <param name="errors">Errors</param>
    public Error(IEnumerable<Error> errors) : this(CommonMessageCodes.Errors.AggregateError)
    {
        this.Errors = errors;
    }

    /// <summary>
    /// Alternate aggregate error constructor
    /// </summary>
    /// <param name="errors">Errors</param>
    public Error(params Error[] errors) : this(CommonMessageCodes.Errors.AggregateError)
    {
        this.Errors = errors;
    }

    /// <summary>
    /// Error constructor from a ResponseCode instance
    /// </summary>
    /// <param name="code">Response code / message definition</param>
    public Error(MessageCode code) : this(code.Code, code.Message)
    { }

    /// <summary>
    /// Error response code
    /// </summary>
    public int ResponseCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Aggregate errors.
    /// </summary>
    public IEnumerable<Error>? Errors { get; set; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Error error &&
               ResponseCode == error.ResponseCode &&
               Message == error.Message &&
               EqualityComparer<IEnumerable<Error>?>.Default.Equals(Errors, error.Errors);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(ResponseCode, Message, Errors);
    }

    /// <inheritdoc />
    public static bool operator ==(Error? left, Error? right)
    {
        return EqualityComparer<Error>.Default.Equals(left, right);
    }

    /// <inheritdoc />
    public static bool operator !=(Error? left, Error? right)
    {
        return !(left == right);
    }
}
