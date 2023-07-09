using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.GeoJson.Exceptions;
using Skybrud.Essentials.Maps.GeoJson.Json;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>MultiPoint</strong> geometry.
    /// </summary>
    [JsonConverter(typeof(GeoJsonConverter))]
    public class GeoJsonMultiPoint : GeoJsonGeometry, IReadOnlyList<GeoJsonCoordinates> {

        private readonly List<GeoJsonCoordinates> _points;

        #region Properties

        /// <summary>
        /// Gets the amount of points in the geometry.
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
        /// Initializes a new empty instance.
        /// </summary>
        public GeoJsonMultiPoint() : base(GeoJsonType.MultiPoint) {
            _points = new List<GeoJsonCoordinates>();
        }

        /// <summary>
        /// Initializes a new instance based on the specified array of <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">An array of coordinates which the <strong>MultiPoint</strong> geometry should be based on.</param>
        public GeoJsonMultiPoint(double[][] coordinates) : base(GeoJsonType.MultiPoint) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            _points = coordinates.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified collection of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The collection of points which the <strong>MultiPoint</strong> geometry should be based on.</param>
        public GeoJsonMultiPoint(IEnumerable<IPoint> points) : base(GeoJsonType.MultiPoint) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points = points.Select(x => new GeoJsonCoordinates(x)).ToList();
        }

        /// <summary>
        /// Initializes a new instance based on the specified collection of <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The collection of coordinates which the <strong>MultiPoint</strong> geometry should be based on.</param>
        public GeoJsonMultiPoint(IEnumerable<GeoJsonCoordinates> coordinates) : base(GeoJsonType.LineString) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            _points = new List<GeoJsonCoordinates>(coordinates);
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the <strong>MultiPoint</strong> geometry.</param>
        protected GeoJsonMultiPoint(JObject json) : base(GeoJsonType.MultiPoint) {
            if (json == null) throw new ArgumentNullException(nameof(json));
            _points = (json.GetArray("coordinates") ?? new JArray())
                .Cast<JArray>()
                .Select(x => new GeoJsonCoordinates(x.GetDouble(0), x.GetDouble(1)))
                .ToList();
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
        /// Adds the specified collection of <paramref name="points"/> to the end of this <strong>MultiPoint</strong> geometry.
        /// </summary>
        /// <param name="points">A collection of points to be added.</param>
        public void AddRange(IEnumerable<IPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points.AddRange(from point in points select new GeoJsonCoordinates(point.Longitude, point.Latitude));
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="points"/> to the end of this <strong>MultiPoint</strong> geometry.
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

        /// <inheritdoc />
        public override IGeometry ToGeometry() {
            throw new GeoJsonException($"The Geometry namespace does not have an equivalent for {nameof(GeoJsonMultiPoint)}.");
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
        /// Parses the specified <paramref name="json"/> string into an instance of <see cref="GeoJsonMultiPoint"/>.
        /// </summary>
        /// <param name="json">The raw JSON string.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiPoint"/>.</returns>
        public static new GeoJsonMultiPoint Parse(string json) {
            if (string.IsNullOrWhiteSpace(json)) throw new ArgumentNullException(nameof(json));
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonMultiPoint"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public static new GeoJsonMultiPoint Parse(JObject json) {
            if (json is null) throw new ArgumentNullException(nameof(json));
            return new GeoJsonMultiPoint(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonMultiPoint"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiPoint"/>.</returns>
        public static new GeoJsonMultiPoint Load(string path) {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}