using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    public class GeoJsonGeometryCollection : GeoJsonGeometry {

        #region Properties

        [JsonProperty("geometries")]
        public List<GeoJsonGeometry> Geometries { get; set; }

        #endregion

        #region Constructors

        public GeoJsonGeometryCollection() : base(GeoJsonType.GeometryCollection) {
            Geometries = new List<GeoJsonGeometry>();
        }

        public GeoJsonGeometryCollection(IEnumerable<GeoJsonGeometry> geometries) : base(GeoJsonType.GeometryCollection) {
            Geometries = geometries?.ToList() ?? new List<GeoJsonGeometry>();
        }
        
        protected GeoJsonGeometryCollection(JObject obj) : base(GeoJsonType.GeometryCollection) {
            Geometries = obj.GetArrayItems("geometries", GeoJsonGeometry.Parse).ToList();
        }

        #endregion

        #region Static methods

        public new static GeoJsonGeometryCollection Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        public new static GeoJsonGeometryCollection Parse(JObject json) {
            return json == null ? null : new GeoJsonGeometryCollection(json);
        }

        #endregion

    }

}