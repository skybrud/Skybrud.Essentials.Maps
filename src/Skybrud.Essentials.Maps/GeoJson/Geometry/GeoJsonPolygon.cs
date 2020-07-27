using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.Extensions;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>Polygon</strong> geometry.
    /// </summary>
    public class GeoJsonPolygon : GeoJsonGeometry {

        #region Properties
        
        /// <summary>
        /// Gets the three-dimensional array representing the outer polygon as well as any inner polygons.
        /// </summary>
        [JsonProperty("coordinates", Order = 100)]
        public double[][][] Coordinates { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">A three-dimensional array representing the outer polygon as well as any inner polygons.</param>
        public GeoJsonPolygon(double[][][] coordinates) : base(GeoJsonType.Polygon) {
            Coordinates = coordinates;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        public GeoJsonPolygon(JObject json) : base(GeoJsonType.Polygon) {
            JArray coordinates = json.GetValue("coordinates") as JArray;
            Coordinates = coordinates == null ? new double[0][][] : coordinates.ToObject<double[][][]>();
        }
        
        /// <summary>
        /// Initializes a new instance from the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public GeoJsonPolygon(IPolygon polygon) : base(GeoJsonType.Polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            Coordinates = MapsUtils.ToXyArray(polygon.GetCoordinates());
        }

        /// <summary>
        /// Initializes a new instance from the specified array of <see cref="IPoint"/> <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public GeoJsonPolygon(IPoint[][] coordinates) : base(GeoJsonType.Polygon) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            Coordinates = MapsUtils.ToXyArray(coordinates);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns an instance of <see cref="IPolygon"/> representing this GeoJSON polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IPolygon"/>.</returns>
        public IPolygon ToPolygon() {
            return new Polygon(Coordinates
                .Select(x => x.Select(y => (IPoint)new Point(y[1], y[0])).ToArray())
                .ToArray());
        }

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            return ToPolygon();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonPolygon Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonPolygon Parse(JObject json) {
            return json == null ? null : new GeoJsonPolygon(json);
        }

        /// <summary>
        /// Loads and parses the polygon at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonPolygon Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}