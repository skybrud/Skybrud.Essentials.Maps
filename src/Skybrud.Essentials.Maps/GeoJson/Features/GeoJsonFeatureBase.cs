using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Essentials.Maps.GeoJson.Features {

    public abstract class GeoJsonFeatureBase : GeoJsonObject {

        #region Properties
        
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public GeoJsonProperties Properties { get; set; }

        #endregion

        #region Constructors

        protected GeoJsonFeatureBase(GeoJsonType type) : base(type) {
            Properties = new GeoJsonProperties();
        }
        
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