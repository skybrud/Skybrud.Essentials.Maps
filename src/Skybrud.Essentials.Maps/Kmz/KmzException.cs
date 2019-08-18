using System;

namespace Skybrud.Essentials.Maps.Kmz {

    /// <summary>
    /// An exception triggered when parsing a KMZ file fails.
    /// </summary>
    public class KmzException : Exception {

        /// <summary>
        /// Initializes a new exception with the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public KmzException(string message) : base(message) { }

    }

}