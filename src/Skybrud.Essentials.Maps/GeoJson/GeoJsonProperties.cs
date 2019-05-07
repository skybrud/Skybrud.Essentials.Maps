using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Json;

namespace Skybrud.Essentials.Maps.GeoJson {

    [JsonConverter(typeof(GeoJsonPropertiesJsonConverter))]
    public class GeoJsonProperties {

        #region Properties

        public int Count => Properties?.Count ?? 0;

        public Dictionary<string, object> Properties { get; }

        public string[] Keys => Properties?.Keys.ToArray() ?? new string[0];

        public object this[string key] {
            get => Properties[key];
            set => Properties[key] = value;
        }

        public string Name {
            get => Properties.TryGetValue("name", out object value) ? value as string : null;
            set => Properties["name"] = value;
        }

        public string Description {
            get => Properties.TryGetValue("description", out object value) ? value as string : null;
            set => Properties["description"] = value;
        }

        
        public string Fill {
            get => Properties.TryGetValue("fill", out object value) ? value as string : null;
            set => Properties["fill"] = value;
        }

        public float FillOpacity {
            get => Properties.TryGetValue("fill-opacity", out object value) ? Convert.ToSingle(value) : 1;
            set => Properties["fill-opacity"] = value;
        }

        public string MarkerColor {
            get => Properties.TryGetValue("marker-color", out object value) ? value as string : null;
            set => Properties["marker-color"] = value;
        }

        public string MarkerSymbol {
            get => Properties.TryGetValue("marker-symbol", out object value) ? value as string : null;
            set => Properties["marker-symbol"] = value;
        }
        
        public string Stroke {
            get => Properties.TryGetValue("stroke", out object value) ? value as string : null;
            set => Properties["stroke"] = value;
        }
        
        public float StrokeWidth {
            get => Properties.TryGetValue("stroke-width", out object value) ? Convert.ToSingle(value) : 1;
            set => Properties["stroke-width"] = value;
        }

        public float StrokeOpacity {
            get => Properties.TryGetValue("stroke-opacity", out object value) ? Convert.ToSingle(value) : 1;
            set => Properties["stroke-opacity"] = value;
        }

        public string MarkerSize {
            get => Properties.TryGetValue("marker-size", out object value) ? value as string : null;
            set => Properties["marker-size"] = value;
        }

        #endregion

        #region Constructors

        public GeoJsonProperties() {
            Properties = new Dictionary<string, object>();
        }

        public GeoJsonProperties(Dictionary<string, object> properties) {
            Properties = properties;
        }

        protected GeoJsonProperties(JObject obj) {
            Properties = obj?.ToObject<Dictionary<string, object>>() ?? new Dictionary<string, object>();
        }

        #endregion

        #region Static methods

        public static GeoJsonProperties Parse(JObject obj) {
            return new GeoJsonProperties(obj);
        }

        #endregion

    }

}