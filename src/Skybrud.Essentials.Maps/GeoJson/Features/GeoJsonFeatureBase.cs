using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Essentials.Maps.GeoJson.Features {

    /// <summary>
    /// Base class with common properties for <see cref="GeoJsonFeature"/> and <see cref="GeoJsonFeatureCollection"/>.
    /// </summary>
    public abstract class GeoJsonFeatureBase : GeoJsonObject {

        #region Properties
        
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public GeoJsonProperties Properties { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the feature.</param>
        protected GeoJsonFeatureBase(GeoJsonType type) : base(type) {
            Properties = new GeoJsonProperties();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="type"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="type">The type of the feature.</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the feature.</param>
        protected GeoJsonFeatureBase(GeoJsonType type, JObject obj) : base(type) {
            Properties = obj.GetObject<GeoJsonProperties>("properties");
        }

        #endregion

        #region Member methods

        public bool ShouldSerializeProperties() {
            return Properties?.Count > 0;
        }

        #endregion

    }

}