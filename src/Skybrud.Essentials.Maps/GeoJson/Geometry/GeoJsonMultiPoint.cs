using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.GeoJson.Geometry {

    public class GeoJsonMultiPoint : GeoJsonGeometry, IEnumerable<double[]> {

        private readonly List<double[]> _points;

        #region Properties

        public int Count => _points.Count;

        #endregion

        #region Constructors

        public GeoJsonMultiPoint() : base(GeoJsonType.MultiPoint) {
            _points = new List<double[]>();
        }

        private GeoJsonMultiPoint(JObject obj) : base(GeoJsonType.MultiPoint) {
            _points = (obj.GetArray("coordinates") ?? new JArray())
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
            return JsonUtils.ParseJsonObject(json, Parse);
        }

        /// <summary>
        /// Parses the specified <paramref name="json"/> object into an instance of <see cref="GeoJsonMultiPoint"/>.
        /// </summary>
        /// <param name="json">The JSON object.</param>
        /// <returns>An instance of <see cref="GeoJsonPolygon"/>.</returns>
        public new static GeoJsonMultiPoint Parse(JObject json) {
            return json == null ? null : new GeoJsonMultiPoint(json);
        }

        public static GeoJsonMultiPoint Load(string path) {
            return JsonUtils.LoadJsonObject(path, Parse);
        }

        #endregion

    }

}