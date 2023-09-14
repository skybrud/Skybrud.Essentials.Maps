namespace Skybrud.Essentials.Maps.Wkt.Exceptions;

/// <summary>
/// Exception used for representing a an error when parsing a string into a <strong>Well Known Text</strong> object.
/// </summary>
public class WktInvalidFormatException : WktException {

    #region Properties

    /// <summary>
    /// Gets the input that resulted in this exception.
    /// </summary>
    public string Input { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance based on the specified <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input that resulteted in the exception.</param>
    public WktInvalidFormatException(string input) : base("Input string is in an invalid format.") {
        Input = input;
    }

    /// <summary>
    /// Initializes a new instance based on the specified <paramref name="input"/> and <paramref name="message"/>.
    /// </summary>
    /// <param name="input">The input that resulteted in the exception.</param>
    /// <param name="message">The message of the exception.</param>
    public WktInvalidFormatException(string input, string message) : base(message) {
        Input = input;
    }

    #endregion

}