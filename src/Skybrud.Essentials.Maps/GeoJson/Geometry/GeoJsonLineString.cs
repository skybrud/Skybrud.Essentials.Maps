using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>LineString</strong> geometry.
    /// </summary>
    [JsonConverter(typeof(GeoJsonConverter))]
    public class GeoJsonLineString : GeoJsonGeometry, IGeoJsonLine, IEnumerable<GeoJsonCoordinates> {

        private readonly List<GeoJsonCoordinates> _points;

        #region Properties

        /// <summary>
        /// Gets the amount of points in the line.
        /// </summary>
        public int Count => _points.Count;

        /// <summary>
        /// Returns the point at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the point to retrieve.</param>
        /// <returns>The <see cref="GeoJsonCoordinates"/> at the specified <paramref name="index"/>.</returns>
        public GeoJsonCoordinates this[int index] => _points.ElementAt(index);

        #endregion

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance with no initial points.
        /// </summary>
        public GeoJsonLineString() : base(GeoJsonType.LineString) {
            _points = new List<GeoJsonCoordinates>();
        }

        /// <summary>
        /// Initializes a new instance based on the specified array of <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">An array of coordinates the <strong>LineString</strong> geometry should be based on.</param>
        public GeoJsonLineString(double[][] coordinates) : base(GeoJsonType.LineString) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            _points = coordinates.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified collection of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The collection of points the <strong>LineString</strong> geometry should be based on.</param>
        public GeoJsonLineString(IEnumerable<IPoint> points) : base(GeoJsonType.MultiPoint) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points = points.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified collection of <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The collection of coordinates the <strong>LineString</strong> geometry should be based on.</param>
        public GeoJsonLineString(IEnumerable<GeoJsonCoordinates> coordinates) : base(GeoJsonType.LineString) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            _points = new List<GeoJsonCoordinates>(coordinates);
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The line the <strong>LineString</strong> geometry should be based on.</param>
        public GeoJsonLineString(ILine line) : base(GeoJsonType.LineString) {
            if (line == null) throw new ArgumentNullException(nameof(line));
            _points = new List<GeoJsonCoordinates> {new GeoJsonCoordinates(line.A), new GeoJsonCoordinates(line.B)};
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="lineString"/>.
        /// </summary>
        /// <param name="lineString">The line string the <strong>LineString</strong> geometry should be based on.</param>
        public GeoJsonLineString(ILineString lineString) : base(GeoJsonType.LineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            _points = lineString.Points.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the <strong>LineString</strong> geometry.</param>
        protected GeoJsonLineString(JObject json) : base(GeoJsonType.LineString) {

            _points = new List<GeoJsonCoordinates>();

            if (!(json.GetValue("coordinates") is JArray array)) {
                return;
            }

            foreach (JToken token in array) {
                _points.Add(new GeoJsonCoordinates(token.Select(x => x.Value<double>()).ToArray()));
            }

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds a new point with the specified <paramref name="x"/> and <paramref name="y"/> coordinates.
        /// </summary>
        /// <param name="x">The coordinate across the X axis.</param>
        /// <param name="y">The coordinate across the Y axis.</param>
        public void Add(double x, double y) {
            _points.Add(new GeoJsonCoordinates(x, y));
        }

        /// <summary>
        /// Adds a new point with the specified <paramref name="x"/> and <paramref name="y"/> coordinates and <paramref name="altitude"/>.
        /// </summary>
        /// <param name="x">The coordinate across the X axis.</param>
        /// <param name="y">The coordinate across the Y axis.</param>
        /// <param name="altitude">The altitude.</param>
        public void Add(double x, double y, double altitude) {
            _points.Add(new GeoJsonCoordinates(x, y, altitude));
        }

        /// <summary>
        /// Adds the specified <paramref name="point"/>. 
        /// </summary>
        /// <param name="point">The point to be added. The array must have a minimum length of two describing both the <c>x</c> and <c>y</c> coordinates of the point, and may optionally specify the altitude as a third item in the array.</param>
        public void Add(double[] point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(new GeoJsonCoordinates(point[0], point[1], point.Length > 2 ? point[2] : 0));
        }

        /// <summary>
        /// Adds the specified <paramref name="point"/>. 
        /// </summary>
        /// <param name="point">The point to be added. Notice that <see cref="IPoint.Latitude"/> equals to <c>y</c> coordinate and <see cref="IPoint.Longitude"/> equals to <c>x</c> coordinate.</param>
        public void Add(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(new GeoJsonCoordinates(point.Longitude, point.Latitude));
        }

        /// <summary>
        /// Adds the specified <paramref name="point"/>. 
        /// </summary>
        /// <param name="point">The point to be added.</param>
        public void Add(GeoJsonCoordinates point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(point);
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="points"/> to the end of this <strong>LineString</strong> geometry.
        /// </summary>
        /// <param name="points">A collection of points to be added.</param>
        public void AddRange(IEnumerable<IPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points.AddRange(from point in points select new GeoJsonCoordinates(point.Longitude, point.Latitude));
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="points"/> to the end of this <strong>LineString</strong> geometry.
        /// </summary>
        /// <param name="points">A collection of points to be added.</param>
        public void AddRange(IEnumerable<GeoJsonCoordinates> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points.AddRange(points);
        }

        /// <summary>
        /// Removes the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to be removed.</param>
        public void Remove(GeoJsonCoordinates point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Remove(point);
        }

        /// <summary>
        /// Removes all points.
        /// </summary>
        public void Clear() {
            _points.Clear();
        }

        /// <summary>
        /// Returns an instance of <see cref="ILineString"/> representing this GeoJSON line string.
        /// </summary>
        /// <returns>An instance of <see cref="ILineString"/>.</returns>
        public ILineString ToLineString() {
            return new LineString(_points.Select(x => x.ToPoint()));
        }

        /// <inheritdoc />
        public ILineBase ToLine() {
            return ToLineString();
        }

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            return ToLineString();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the underlying <see cref="List{Double}"/>.
        /// </summary>
        /// <returns>A <see cref="List{GeoJsonCoordinates}.Enumerator"/> for the interal <see cref="List{GeoJsonCoordinates}"/>.</returns>
        public IEnumerator<GeoJsonCoordinates> GetEnumerator() {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Parse(JObject json) {
            return json == null ? null : new GeoJsonLineString(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonLineString"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonLineString"/>.</returns>
        public new static GeoJsonLineString Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}