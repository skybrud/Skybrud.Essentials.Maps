using System;

namespace Skybrud.Essentials.Maps.Kml.Exceptions {

    /// <summary>
    /// Class representing an exception thrown by the KML related logic.
    /// </summary>
    public class KmlException : Exception {

        /// <summary>
        /// Initializes a new exception with the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public KmlException(string message) : base(message) { }

    }

}