using System;
using Newtonsoft.Json.Linq;

namespace Skybrud.Essentials.Maps.GeoJson.Exceptions {

    public class GeoJsonParseException : GeoJsonException {

        public JObject JObject { get; }

        public GeoJsonParseException(string message) : base(message) { }

        public GeoJsonParseException(string message, JObject obj) : base(message) {
            JObject = obj;
        }

        public GeoJsonParseException(string message, Exception innerException) : base(message, innerException) { }

        public GeoJsonParseException(string message, JObject obj, Exception innerException) : base(message, innerException) {
            JObject = obj;
        }

    }

}