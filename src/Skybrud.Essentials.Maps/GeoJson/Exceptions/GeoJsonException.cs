using System;

namespace Skybrud.Essentials.Maps.GeoJson.Exceptions {

    /// <summary>
    /// Class representing an exception related to the <see cref="GeoJson"/> namespace.
    /// </summary>
    public class GeoJsonException : Exception {

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoJsonException"/> class with a specified error <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GeoJsonException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoJsonException"/> class with a specified error <paramref name="message"/> and a reference to the <paramref name="innerException"/> that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public GeoJsonException(string message, Exception? innerException) : base(message, innerException) { }

    }

}