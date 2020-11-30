using System;

namespace Skybrud.Essentials.Maps.GeoJson.Exceptions {
    
    public class GeoJsonParseException : Exception {

        public GeoJsonParseException(string message) : base(message) { }

        public GeoJsonParseException(string message, Exception innerException) : base(message, innerException) { }

    }

}