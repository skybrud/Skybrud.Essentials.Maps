using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Maps.GeoJson.Json;

namespace Skybrud.Essentials.Maps.GeoJson {

    /// <summary>
    /// Base class for all <strong>GeoJSON</strong> classes.
    /// </summary>
    [JsonConverter(typeof(GeoJsonReadConverter))]
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
            return JsonConvert.SerializeObject(this, formatting, new JsonSerializerSettings {
                ContractResolver = GeoJsonContractResolver.Instance
            });
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

        #region Static methods

        /// <summary>
        /// Parses the specified the specified <paramref name="json"/> into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <param name="json">The JSON string to be parsed.</param>
        /// <param name="callback">A callback function/method used for converting an instance of <see cref="JObject"/> into an instance of <typeparamref name="T"/>.</param>
        /// <returns>An instance of <typeparamref name="T"/> parsed from the specified JSON string.</returns>
        protected static T ParseJsonObject<T>(string json, Func<JObject, T> callback) {
            return JsonUtils.ParseJsonObject(json, callback);
        }

        /// <summary>
        /// Loads and parses the JSON object in the file at the specified <paramref name="path"/>.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <param name="path">The path to the JSON file.</param>
        /// <param name="callback">A callback function/method used for converting an instance of <see cref="JObject"/> into an instance of <typeparamref name="T"/>.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        protected static T LoadJsonObject<T>(string path, Func<JObject, T> callback) {
            return JsonUtils.LoadJsonObject(path, callback);
        }

        #endregion

    }

}