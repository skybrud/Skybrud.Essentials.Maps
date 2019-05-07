using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    public class GeoJsonLineString : GeoJsonGeometry {

        #region Properties

        [JsonProperty("coordinates", Order = 100)]
        public double[][] Coordinates { get; set; }

        #endregion

        #region Constructors
        
        public GeoJsonLineString(double[][] coordinates) : base(GeoJsonType.LineString) {
            Coordinates = coordinates;
        }

        protected GeoJsonLineString(JObject obj) : base(GeoJsonType.LineString) {
            JArray coordinates = obj.GetValue("coordinates") as JArray;
            Coordinates = coordinates == null ? new double[0][] : coordinates.ToObject<double[][]>();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Parse(JObject json) {
            return json == null ? null : new GeoJsonLineString(json);
        }

        #endregion

    }

}