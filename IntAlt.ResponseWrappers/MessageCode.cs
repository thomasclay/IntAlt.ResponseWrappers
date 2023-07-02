namespace IntAlt.ResponseWrappers;

/// <summary>
/// For defining a code + message - useful for constant / static descriptors.
/// </summary>
/// <param name="Code">Response code</param>
/// <param name="Message">Descriptive message</param>
public record MessageCode(int Code, string Message);
