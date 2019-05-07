using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Maps.Extensions;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a <strong>GeoJSON</strong> polygon.
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
        /// Initislizes a new instance from the specified JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        public GeoJsonPolygon(JObject obj) : base(GeoJsonType.Polygon) {
            JArray coordinates = obj.GetValue("coordinates") as JArray;
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

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonPolygon Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonPolygon"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonPolygon Parse(JObject json) {
            return json == null ? null : new GeoJsonPolygon(json);
        }

        #endregion

    }

}