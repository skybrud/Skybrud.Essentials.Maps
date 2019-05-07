using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Features {

    public class GeoJsonFeature : GeoJsonObject {

        #region Properties
        
        [JsonProperty("properties")]
        public GeoJsonProperties Properties { get; set; }

        [JsonProperty("geometry")]
        public GeoJsonGeometry Geometry { get; set; }

        #endregion

        #region Constructors

        public GeoJsonFeature() : base(GeoJsonType.Feature) {
            Properties = new GeoJsonProperties();
        }

        protected GeoJsonFeature(JObject obj) : base(GeoJsonType.Feature) {
            Properties = obj.GetObject("properties", GeoJsonProperties.Parse);
            Geometry = obj.GetObject<GeoJsonGeometry>("geometry");
        }

        #endregion

        #region Static methods

        public static GeoJsonFeature Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        public static GeoJsonFeature Parse(JObject json) {
            return json == null ? null : new GeoJsonFeature(json);
        }

        public static GeoJsonFeature Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}