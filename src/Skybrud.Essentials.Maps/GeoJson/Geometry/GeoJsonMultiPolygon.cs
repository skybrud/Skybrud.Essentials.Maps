using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    public class GeoJsonMultiPolygon : GeoJsonGeometry {

        #region Properties

        /// <summary>
        /// Gets the four-dimensional array representing the polygons.
        /// </summary>
        [JsonProperty("coordinates", Order = 100)]
        public double[][][][] Coordinates { get; set; }

        #endregion

        #region Constructors

        public GeoJsonMultiPolygon() : base(GeoJsonType.MultiPolygon) {
            Coordinates = new double[0][][][];
        }

        public GeoJsonMultiPolygon(double[][][][] coordinates) : base(GeoJsonType.MultiPolygon) {
            Coordinates = coordinates;
        }

        public GeoJsonMultiPolygon(JObject obj) : base(GeoJsonType.MultiPolygon) {
            JArray coordinates = obj.GetValue("coordinates") as JArray;
            Coordinates = coordinates == null ? new double[0][][][] : coordinates.ToObject<double[][][][]>();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonMultiPolygon"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonMultiPolygon Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonMultiPolygon"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonMultiPolygon Parse(JObject json) {
            return json == null ? null : new GeoJsonMultiPolygon(json);
        }

        public static GeoJsonMultiPolygon Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}