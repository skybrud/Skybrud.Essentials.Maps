namespace Skybrud.Essentials.Maps.Wkt {

    /// <summary>
    /// Enum class indicating the format to be used when converting <strong>WKT</strong> objects to string.
    /// </summary>
    public enum WktFormatting {

        /// <summary>
        /// Indicates that the default formatting should be used. This includes whitespaces to improve readability.
        /// </summary>
        Default,

        /// <summary>
        /// Indicates that the generated string should be mindified. This means no whitespaces.
        /// </summary>
        Minified,

        /// <summary>
        /// Indicates that the the generated string should be indented. This means both whitespaces and line breaks to make the result more readable.
        /// </summary>
        Indented

    }

}