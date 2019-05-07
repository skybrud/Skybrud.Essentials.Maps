using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Features;

namespace Skybrud.Essentials.Maps.GeoJson.Json {

    public class GeoJsonObjectJsonConverter : JsonConverter {

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

                case "Feature":
                    //return new GeoJsonFeature(obj);
                    throw new NotImplementedException();

                case "FeatureCollection":
                    return new GeoJsonFeatureCollection(obj);

                default:
                    throw new Exception("Unknown object: " + type);

            }

        }

        public override bool CanConvert(Type objectType) {
            return false;
        }

    }

}