using Newtonsoft.Json;

namespace Skybrud.Essentials.Maps.GeoJson {

    //[JsonConverter(typeof(GeoJsonObjectJsonConverter))]
    public class GeoJsonObject {

        #region Properties

        /// <summary>
        /// Gets the type of the object - eg. <see cref="GeoJsonType.Feature"/>.
        /// </summary>
        [JsonProperty("type", Order = -999)]
        public GeoJsonType Type { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        protected GeoJsonObject(GeoJsonType type) {
            Type = type;
        }

        #endregion

        #region Member methods

        public string ToJson() {
            return ToJson(Formatting.Indented);
        }

        public string ToJson(Formatting formatting) {
            return JsonConvert.SerializeObject(this, formatting);
        }

        #endregion

    }

}