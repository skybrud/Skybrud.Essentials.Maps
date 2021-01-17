﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.Extensions;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    /// <summary>
    /// Class representing a GeoJSON <strong>MultiPoint</strong> geometry.
    /// </summary>
    public class GeoJsonMultiPoint : GeoJsonGeometry, IEnumerable<double[]> {

        private readonly List<double[]> _points;

        #region Properties

        /// <summary>
        /// Gets the amount of points in the geometry.
        /// </summary>
        public int Count => _points.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance.
        /// </summary>
        public GeoJsonMultiPoint() : base(GeoJsonType.MultiPoint) {
            _points = new List<double[]>();
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the <strong>MultiPoint</strong> geometry.</param>
        protected GeoJsonMultiPoint(JObject json) : base(GeoJsonType.MultiPoint) {
            _points = (json.GetArray("coordinates") ?? new JArray())
                .Cast<JArray>()
                .Select(x => new [] {x.GetDouble(0), x.GetDouble(1)})
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
            _points.Add(new[] { x, y });
        }

        /// <summary>
        /// Adds the specified <paramref name="point"/>. 
        /// </summary>
        /// <param name="point">The point to be added. The array must have a minimum length of two describing both the <c>x</c> and <c>y</c> coordinates of the point, and may optionally specify the altitude as a third item in the array.</param>
        public void Add(double[] point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(point);
        }

        /// <summary>
        /// Adds the specified <paramref name="point"/>. 
        /// </summary>
        /// <param name="point">The point to be added. Notice that <see cref="IPoint.Latitude"/> equals to <c>y</c> coordinate and <see cref="IPoint.Longitude"/> equals to <c>x</c> coordinate.</param>
        public void Add(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(new[] { point.Longitude, point.Latitude });
        }

        /// <summary>
        /// Adds the specified collection of <paramref name="points"/> to the end of this <strong>MultiPoint</strong> geometry.
        /// </summary>
        /// <param name="points">A collection of points to be added.</param>
        public void AddRange(IEnumerable<IPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points.AddRange(from point in points select point.ToXyArray());
        }

        /// <summary>
        /// Removes the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to be removed.</param>
        public void Remove(double[] point) {
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
            throw new Exception($"The Geometry namespace does not have an equivalent for {nameof(GeoJsonMultiPoint)}.");
        }

        /// <summary>
        /// Returns an enumerator that iterates through the underlying <see cref="List{Double}"/>.
        /// </summary>
        /// <returns>A System.Collections.Generic.List`1.Enumerator for the System.Collections.Generic.List`1.</returns>
        public IEnumerator<double[]> GetEnumerator() {
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
        public new static GeoJsonMultiPoint Parse(string json) {
            return ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonMultiPoint"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonMultiPoint Parse(JObject json) {
            return json == null ? null : new GeoJsonMultiPoint(json);
        }

        /// <summary>
        /// Loads and parses the feature at the specified <paramref name="path"/> into an instance of <see cref="GeoJsonMultiPoint"/>.
        /// </summary>
        /// <param name="path">The path to a file on disk.</param>
        /// <returns>An instance of <see cref="GeoJsonMultiPoint"/>.</returns>
        public new static GeoJsonMultiPoint Load(string path) {
            return LoadJsonObject(path, Parse);
        }

        #endregion

    }

}