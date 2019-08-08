using System.Text;
using Newtonsoft.Json;

namespace Skybrud.Essentials.Maps.GeoJson {

    //[JsonConverter(typeof(GeoJsonObjectJsonConverter))]
    public abstract class GeoJsonObject {

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

        /// <summary>
        /// Returns a JSON representation of this instance.
        /// </summary>
        /// <returns>The generated JSON string.</returns>
        public string ToJson() {
            return ToJson(Formatting.Indented);
        }

        /// <summary>
        /// Returns a JSON representation of this instance.
        /// </summary>
        /// <param name="formatting">Indicates how the output should be formatted.</param>
        /// <returns>The generated JSON string.</returns>
        public string ToJson(Formatting formatting) {
            return JsonConvert.SerializeObject(this, formatting);
        }

        /// <summary>
        /// Saves a JSON representation of this instance to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to which the JSON should be saved.</param>
        public virtual void Save(string path) {
            Save(path, Formatting.Indented);
        }

        /// <summary>
        /// Saves a JSON representation of this instance to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to which the JSON should be saved.</param>
        /// <param name="formatting">Indicates how the output should be formatted.</param>
        public virtual void Save(string path, Formatting formatting) {
            System.IO.File.WriteAllText(path, ToJson(formatting), Encoding.UTF8);
        }

        #endregion

    }

}