using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Essentials.Maps.GeoJson.Json {

    /// <summary>
    /// JSON converter class for deserializing and deserializing objects within the <c>Skybrud.Essentials.Maps.GeoJson</c> namespace.
    /// </summary>
    public class GeoJsonWriteConverter : JsonConverter {

        #region Properties

        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override bool CanRead => false;

        #endregion

        #region Member methods

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            if (value == null) {
                writer.WriteNull();
                return;
            }

            switch (value) {

                case GeoJsonCoordinates coordinates:
                    JArray.FromObject(coordinates.ToArray()).WriteTo(writer);
                    return;

                case List<GeoJsonCoordinates> list:
                    JArray.FromObject(list.Select(x => x.ToArray())).WriteTo(writer);
                    return;

                case GeoJsonProperties properties:
                    
                    Dictionary<string, object> temp = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, object> pair in properties.Properties) {
                        if (pair.Value == null) continue;
                        temp.Add(pair.Key, pair.Value);
                    }
                    
                    JObject.FromObject(temp).WriteTo(writer);
                    return;

                default:
                    writer.WriteNull();
                    return;

            }



        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();

        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(GeoJsonCoordinates) || objectType == typeof(List<GeoJsonCoordinates>);
        }

        #endregion

    }

}