using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.GeoJson.Features;
using Skybrud.Essentials.Maps.GeoJson.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Json {

    /// <summary>
    /// JSON converter class for deserializing and deserializing objects within the <c>Skybrud.Essentials.Maps.GeoJson</c> namespace.
    /// </summary>
    public class GeoJsonReadConverter : JsonConverter {

        #region Properties

        /// <inheritdoc />
        public override bool CanWrite => false;

        /// <inheritdoc />
        public override bool CanRead => true;

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

            if (reader.TokenType == JsonToken.Null) return null;
            if (reader.TokenType != JsonToken.StartObject) return null;

            JObject obj = JObject.Load(reader);

            string type = obj.Value<string>("type");

            switch (type?.ToLower()) {

                case "feature":
                    return GeoJsonFeature.Parse(obj);

                case "featurecollection":
                    return GeoJsonFeatureCollection.Parse(obj);

                case "point":
                    return GeoJsonPoint.Parse(obj);

                case "multipoint":
                    return GeoJsonMultiPoint.Parse(obj);

                case "linestring":
                    return GeoJsonLineString.Parse(obj);

                case "multilinestring":
                    return GeoJsonMultiLineString.Parse(obj);

                case "polygon":
                    return GeoJsonPolygon.Parse(obj);

                case "multipolygon":
                    return GeoJsonMultiPolygon.Parse(obj);

                default:
                    if (objectType == typeof(GeoJsonProperties)) {
                        return ReadJsonProperties(reader);
                    }
                    throw new GeoJsonParseException($"Unknown shape: {type}", obj);

            }


        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) {
            return true;
        }

        private object ReadJsonProperties(JsonReader reader) {

            JObject obj = JObject.Load(reader);

            Dictionary<string, object> temp = obj.ToObject<Dictionary<string, object>>();

            return new GeoJsonProperties(temp);

        }

        #endregion

    }

}