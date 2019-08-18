using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Features {

    /// <summary>
    /// Class representing a GeoJSON <strong>Feature</strong>.
    /// </summary>
    public class GeoJsonFeature : GeoJsonObject {

        #region Properties
        
        /// <summary>
        /// Gets or sets the properties of the feature.
        /// </summary>
        [JsonProperty("properties")]
        public GeoJsonProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets the geometry of the feature.
        /// </summary>
        [JsonProperty("geometry")]
        public GeoJsonGeometry Geometry { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        public GeoJsonFeature() : base(GeoJsonType.Feature) {
            Properties = new GeoJsonProperties();
        }

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="geometry"/>.
        /// </summary>
        /// <param name="geometry">The geometry of the feature.</param>
        public GeoJsonFeature(GeoJsonGeometry geometry) : base(GeoJsonType.Feature) {
            Geometry = geometry;
            Properties = new GeoJsonProperties();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the feature.</param>
        protected GeoJsonFeature(JObject obj) : base(GeoJsonType.Feature) {
            Properties = obj.GetObject("properties", GeoJsonProperties.Parse);
            Geometry = obj.GetObject<GeoJsonGeometry>("geometry");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonFeature"/>.
        /// </summary>
        /// <param name="json">The JSON string to be parsed.</param>
        /// <returns>An instance of <see cref="GeoJsonFeature"/>.</returns>
        public static GeoJsonFeature Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonFeature"/>.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GeoJsonFeature"/>.</returns>
        public static GeoJsonFeature Parse(JObject json) {
            return json == null ? null : new GeoJsonFeature(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonFeature"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonFeature"/>.</returns>
        public static GeoJsonFeature Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}