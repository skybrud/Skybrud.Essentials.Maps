using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Essentials.Maps.GeoJson.Json {

    public class GeoJsonPropertiesJsonConverter : JsonConverter {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            if (value == null) {
                writer.WriteNull();
                return;
            }

            if (value is GeoJsonProperties) {

                var temp = (GeoJsonProperties) value;

                Dictionary<string, object> obj = new Dictionary<string, object>();

                foreach (var pair in temp.Properties) {
                    if (pair.Value == null) continue;
                    obj.Add(pair.Key, pair.Value);
                }

                obj.Add("style", new {
                    color = temp.Stroke,
                    weight = temp.StrokeWidth
                });

                if (obj.Count > 0) {
                    serializer.Serialize(writer, obj);
                }

            }

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

            if (reader.TokenType == JsonToken.Null) return null;
            if (reader.TokenType != JsonToken.StartObject) return null;
            
            if (objectType == typeof(GeoJsonProperties))
            {

                return ReadJsonProperties(reader);

            }

            return null;

        }

        private object ReadJsonProperties(JsonReader reader) {

            JObject obj = JObject.Load(reader);

            Dictionary<string, object> temp = obj.ToObject<Dictionary<string, object>>();

            return new GeoJsonProperties(temp);

        }

        public override bool CanConvert(Type objectType) {
            return false;
        }

    }

}