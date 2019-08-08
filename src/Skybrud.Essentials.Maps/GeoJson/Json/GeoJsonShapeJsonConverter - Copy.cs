using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Json {

    public class GeoJsonShapeJsonConverter : JsonConverter {

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

            if (reader.TokenType == JsonToken.Null) return null;
            if (reader.TokenType != JsonToken.StartObject) return null;
            
            JObject obj = JObject.Load(reader);

            string type = obj.Value<string>("type");

            switch (type) {

                case "Point":
                    //return new GeoJsonPoint(obj);
                    throw new NotImplementedException();

                case "LineString":
                    //return new GeoJsonLineString(obj);
                    throw new NotImplementedException();

                case "Polygon":
                    return new GeoJsonPolygon(obj);

                case "MultiPolygon":
                    return new GeoJsonMultiPolygon(obj);

                default:
                    throw new Exception("Unknown shape: " + type);

            }

        }

        public override bool CanConvert(Type objectType) {
            return false;
        }

    }

}