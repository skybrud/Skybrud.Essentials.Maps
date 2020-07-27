using System;

namespace Skybrud.Essentials.Maps.Wkt.Exceptions {
    
    /// <summary>
    /// Exception used for representing errors in the <strong>Well Known Text</strong> implementation.
    /// </summary>
    public class WktException : Exception {

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        public WktException(string message) : base(message) { }

    }

}