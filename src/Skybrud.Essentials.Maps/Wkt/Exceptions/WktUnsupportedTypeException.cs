using System;

namespace Skybrud.Essentials.Maps.Wkt.Exceptions {

    /// <summary>
    /// Exception class representing an parse error due to an unknown or unsupported <strong>Well Known Text</strong> geometry.
    /// </summary>
    public class WktUnsupportedTypeException : WktException {

        #region Properties

        /// <summary>
        /// Gets the type of the geometry that resulted in this exception.
        /// </summary>
        public string Type { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the geometry that resulted in this exception.</param>
        public WktUnsupportedTypeException(string type) : base($"Unsupported type: {type}") {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="type"/> and <paramref name="message"/>.
        /// </summary>
        /// <param name="type">The type of the geometry that resulted in this exception.</param>
        /// <param name="message">The message of the exception.</param>
        public WktUnsupportedTypeException(string type, string message) : base(message) {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the geometry that resulted in this exception.</param>
        public WktUnsupportedTypeException(Type type) : base($"Unsupported type: {type.FullName}") {
            Type = type.FullName ?? type.Name;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="type"/> and <paramref name="message"/>.
        /// </summary>
        /// <param name="type">The type of the geometry that resulted in this exception.</param>
        /// <param name="message">The message of the exception.</param>
        public WktUnsupportedTypeException(Type type, string message) : base(message) {
            Type = type.FullName ?? type.Name;
        }

        #endregion

    }

}