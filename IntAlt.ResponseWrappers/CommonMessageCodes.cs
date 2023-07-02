namespace IntAlt.ResponseWrappers;

/// <summary>
/// Common response code descriptors. Will range from -499 to 499.
/// </summary>
public static class CommonMessageCodes
{
    /// <summary>
    /// Success / positive result codes.
    /// </summary>
    public static class Successes
    {
        /// <summary>
        /// General success
        /// </summary>
        public static readonly MessageCode Success = new(0, "Success");

        /// <summary>
        /// Typically, when a request for a record update occurs, but the record already has the new values.
        /// </summary>
        public static readonly MessageCode NoChanges = new(1, "No changes made");

        /// <summary>
        /// The record already exists - no changes are made.
        /// </summary>
        public static readonly MessageCode RecordExists = new(2, "Record exists - no changes made");

        /// <summary>
        /// The record already exists - no changes are made.
        /// </summary>
        public static readonly MessageCode RecordDoesExists = new(3, "Record does not exist - no changes made");
    }

    /// <summary>
    /// Failure / negative result codes.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// General failure - complete failure do to unforeseen causes
        /// </summary>
        public static readonly MessageCode Failure = new(-1, "Failure");

        /// <summary>
        /// Attempt to create a record when it already exists by primary key or other unique constraint
        /// </summary>
        public static readonly MessageCode RecordExists = new(-2, "Record exists");

        /// <summary>
        /// Record not found.
        /// </summary>
        public static readonly MessageCode RecordDoesNotExist = new(-3, "Record does not exist");

        /// <summary>
        /// Aggregate / multiple errors.
        /// </summary>
        public static readonly MessageCode AggregateError = new(-4, "Multiple errors occurred");
    }
}