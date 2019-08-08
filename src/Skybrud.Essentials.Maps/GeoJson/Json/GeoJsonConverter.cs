using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Essentials.Maps.GeoJson.Json {

    public class GeoJsonConverter : JsonConverter {

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            if (value == null) {
                writer.WriteNull();
                return;
            }

            switch (value) {

                case GeoJsonCoordinates coordinates:
                    JArray.FromObject(coordinates.ToArray()).WriteTo(writer);
                    break;

                case List<GeoJsonCoordinates> list:
                    JArray.FromObject(list.Select(x => x.ToArray())).WriteTo(writer);
                    break;

            }

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(GeoJsonCoordinates);
        }

    }

}