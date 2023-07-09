namespace Skybrud.Essentials.Maps.Kml.Exceptions {

    public class KmlParseException : KmlException {

        /// <summary>
        /// Initializes a new exception with the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public KmlParseException(string message) : base(message) { }

    }

}