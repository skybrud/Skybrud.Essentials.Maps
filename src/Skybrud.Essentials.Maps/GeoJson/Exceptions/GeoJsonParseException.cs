using System;
using Newtonsoft.Json.Linq;

namespace Skybrud.Essentials.Maps.GeoJson.Exceptions {

    public class GeoJsonParseException : GeoJsonException {

        public JObject? JObject { get; }

        public GeoJsonParseException(string message) : base(message) { }

        public GeoJsonParseException(string message, JObject json) : base(message) {
            JObject = json;
        }

        public GeoJsonParseException(string message, Exception? innerException) : base(message, innerException) { }

        public GeoJsonParseException(string message, JObject? json, Exception? innerException) : base(message, innerException) {
            JObject = json;
        }

    }

}