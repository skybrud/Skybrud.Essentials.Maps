using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Essentials.Maps.GeoJson.Features {

    /// <summary>
    /// Base class with common properties for <see cref="GeoJsonFeature"/> and <see cref="GeoJsonFeatureCollection"/>.
    /// </summary>
    public abstract class GeoJsonFeatureBase : GeoJsonObject, IGeoJsonFeature {

        #region Properties
        
        /// <summary>
        /// Gets or sets the properties object of this feature.
        /// </summary>
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
        /// Initializes a new instance based on the specified <paramref name="type"/> and <paramref name="json"/>.
        /// </summary>
        /// <param name="type">The type of the feature.</param>
        /// <param name="json">An instance of <see cref="JObject"/> representing the feature.</param>
        protected GeoJsonFeatureBase(GeoJsonType type, JObject json) : base(type) {
            Properties = json.GetObject<GeoJsonProperties>("properties");
        }

        #endregion

    }

}