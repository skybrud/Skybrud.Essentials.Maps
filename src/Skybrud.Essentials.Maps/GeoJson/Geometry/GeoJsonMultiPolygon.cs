using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {


    /// <summary>
    /// Class representing a GeoJSON <strong>MultiPolygon</strong> geometry.
    /// </summary>
    public class GeoJsonMultiPolygon : GeoJsonGeometry {

        #region Properties

        /// <summary>
        /// Gets the four-dimensional array representing the polygons.
        /// </summary>
        [JsonProperty("coordinates", Order = 100)]
        public double[][][][] Coordinates { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        public GeoJsonMultiPolygon() : base(GeoJsonType.MultiPolygon) {
            Coordinates = new double[0][][][];
        }

        /// <summary>
        /// Initializes a instance with the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates of the <strong>MultiPolygon</strong> geometry.</param>
        public GeoJsonMultiPolygon(double[][][][] coordinates) : base(GeoJsonType.MultiPolygon) {
            Coordinates = coordinates;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the <strong>MultiPolygon</strong> geometry.</param>
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

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonMultiPolygon"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiPolygon"/>.</returns>
        public static GeoJsonMultiPolygon Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}