using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Essentials.Maps.GeoJson.Features {

    public class GeoJsonFeatureCollection : GeoJsonObject {

        #region Properties
        
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("features")]
        public List<GeoJsonFeature> Features { get; set; }

        #endregion

        #region Constructors

        public GeoJsonFeatureCollection() : base(GeoJsonType.FeatureCollection) {
            Features = new List<GeoJsonFeature>();
        }

        public GeoJsonFeatureCollection(params GeoJsonFeature[] features) : base(GeoJsonType.FeatureCollection) {
            Features = features?.ToList() ?? new List<GeoJsonFeature>();
        }

        public GeoJsonFeatureCollection(IEnumerable<GeoJsonFeature> features) : base(GeoJsonType.FeatureCollection) {
            Features = features?.ToList() ?? new List<GeoJsonFeature>();
        }
        
        public GeoJsonFeatureCollection(JObject obj) : base(GeoJsonType.FeatureCollection) {
            Name = obj.GetString("name");
            Features = obj.GetArrayItems("features", GeoJsonFeature.Parse).ToList();
        }

        #endregion

        #region Member methods

        public void Add(GeoJsonFeature feature) {
            if (feature == null) throw new ArgumentNullException(nameof(feature));
            Features.Add(feature);
        }

        public void AddRange(IEnumerable<GeoJsonFeature> features) {
            if (features == null) throw new ArgumentNullException(nameof(features));
            Features.AddRange(features);
        }

        public void Remove(GeoJsonFeature feature) {
            if (feature == null) throw new ArgumentNullException(nameof(feature));
            Features.Remove(feature);
        }

        public bool Contains(GeoJsonFeature feature) {
            if (feature == null) throw new ArgumentNullException(nameof(feature));
            return Features.Contains(feature);
        }

        #endregion

        #region Static methods

        public static GeoJsonFeatureCollection Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        public static GeoJsonFeatureCollection Parse(JObject json) {
            return json == null ? null : new GeoJsonFeatureCollection(json);
        }

        public static GeoJsonFeatureCollection Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}