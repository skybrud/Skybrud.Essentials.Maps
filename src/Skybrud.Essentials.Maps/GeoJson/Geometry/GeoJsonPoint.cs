using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>Point</strong> geometry.
    /// </summary>
    public class GeoJsonPoint : GeoJsonGeometry {

        #region Properties

        /// <summary>
        /// Gets or sets the <strong>X</strong> coordinate of the point.
        /// </summary>
        [JsonIgnore]
        public double X {
            get => Coordinates?.X ?? 0;
            set => (Coordinates ?? (Coordinates = new GeoJsonCoordinates())).X = value;
        }

        /// <summary>
        /// Gets or sets the <strong>Y</strong> coordinate of the point.
        /// </summary>
        [JsonIgnore]
        public double Y {
            get => Coordinates?.Y ?? 0;
            set => (Coordinates ?? (Coordinates = new GeoJsonCoordinates())).Y = value;
        }

        /// <summary>
        /// Gets or sets the altitude of the point.
        /// </summary>
        [JsonIgnore]
        public double Altitude {
            get => Coordinates?.Altitude ?? 0;
            set => (Coordinates ?? (Coordinates = new GeoJsonCoordinates())).Altitude = value;
        }

        /// <summary>
        /// Gets or sets the coordinates of the point.
        /// </summary>
        [JsonProperty("coordinates", Order = 100)]
        [JsonConverter(typeof(GeoJsonConverter))]
        public GeoJsonCoordinates Coordinates { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new point with <c>0</c> for both the <see cref="X"/> and <see cref="Y"/> coordinates.
        /// </summary>
        public GeoJsonPoint() : base(GeoJsonType.Point) {
            Coordinates = new GeoJsonCoordinates();
        }

        /// <summary>
        /// Initializes a new point based on the specified <paramref name="x"/> and <paramref name="y"/> coordinates.
        /// </summary>
        /// <param name="x">The <strong>x</strong> (longitude) coordinate.</param>
        /// <param name="y">The <strong>y</strong> (latitude) coordinate.</param>
        public GeoJsonPoint(double x, double y) : base(GeoJsonType.Point) {
            Coordinates = new GeoJsonCoordinates(x, y);
        }

        /// <summary>
        /// Initializes a new point based on the specified <paramref name="x"/> and <paramref name="y"/> coordinates, as wel as the specified <paramref name="altitude"/>.
        /// </summary>
        /// <param name="x">The <strong>x</strong> (longitude) coordinate.</param>
        /// <param name="y">The <strong>y</strong> (latitude) coordinate.</param>
        /// <param name="altitude">The altitude of the point.</param>
        public GeoJsonPoint(double x, double y, double altitude) : base(GeoJsonType.Point) {
            Coordinates = new GeoJsonCoordinates(x, y, altitude);
        }
        
        /// <summary>
        /// Initializes a new point from the specified array of <paramref name="coordinates"/>.
        ///
        /// The first and second items in the array represents the <strong>x</strong> and <strong>y</strong> coordinates respectively. A third item may be used to specify the altitude of the point.
        /// </summary>
        /// <param name="coordinates">The array representing the coordinates.</param>
        public GeoJsonPoint(double[] coordinates) : base(GeoJsonType.Point) {
            Coordinates = new GeoJsonCoordinates(coordinates);
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">An instance of <see cref="IPoint"/> representing the point.</param>
        public GeoJsonPoint(IPoint point) : base(GeoJsonType.Point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            Coordinates = new GeoJsonCoordinates(point);
        }

        private GeoJsonPoint(JObject obj) : base(GeoJsonType.Point) {
            if (!(obj.GetValue("coordinates") is JArray array)) return;
            Coordinates = new GeoJsonCoordinates(array.ToObject<double[]>());
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns an instance of <see cref="IPoint"/> representing this GeoJSON point.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint ToPoint() {
            return new Point(Y, X);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonPoint"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonPoint"/>.</returns>
        public new static GeoJsonPoint Parse(string json) {
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonPoint"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPoint"/>.</returns>
        public new static GeoJsonPoint Parse(JObject json) {
            return json == null ? null : new GeoJsonPoint(json);
        }

        #endregion

    }

}