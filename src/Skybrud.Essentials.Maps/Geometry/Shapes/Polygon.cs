using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Skybrud.Essentials.Maps.Geometry.Shapes {

    public class Polygon : IPolygon {

        private readonly List<IPoint> _outer;

        private readonly List<IPoint[]> _inner;

        #region Properties

        /// <summary>
        /// Gets the array of points making up the outer polygon.
        /// </summary>
        public IPoint[] Outer => _outer.ToArray();

        /// <summary>
        /// Gets the array of points making up any inner polygons.
        /// </summary>
        public IPoint[][] Inner => _inner.ToArray();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new polyline from the specified array of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the polyline should be based on.</param>
        public Polygon(params IPoint[] points) {
            _outer = points?.ToList() ?? throw new ArgumentNullException(nameof(points));
            _inner = new List<IPoint[]>();
        }

        /// <summary>
        /// Initializes a new polyline from the specified array of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the polyline should be based on.</param>
        public Polygon(params Point[] points) {
            _outer = points?.Cast<IPoint>().ToList() ?? throw new ArgumentNullException(nameof(points));
            _inner = new List<IPoint[]>();
        }

        /// <summary>
        /// Initializes a new polyline from the specified collection of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the polyline should be based on.</param>
        public Polygon(IEnumerable<IPoint> points) {
            _outer = points?.ToList() ?? throw new ArgumentNullException(nameof(points));
            _inner = new List<IPoint[]>();
        }
        
        /// <summary>
        /// Initializes a new instance from the specified <paramref name="outer"/> and <paramref name="inner"/> coordinates.
        /// </summary>
        /// <param name="outer">The outer coordinates.</param>
        /// <param name="inner">The inner coordinates.</param>
        public Polygon(IEnumerable<IPoint> outer, params IEnumerable<IPoint>[] inner) {
            _outer = outer?.ToList() ?? throw new ArgumentNullException(nameof(outer));
            _inner = inner?.Select(x => x.ToArray()).ToList() ?? new List<IPoint[]>();
        }

        /// <summary>
        /// Initializes a new instance from the specified array of coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public Polygon(IPoint[][] coordinates) {
            _outer = coordinates[0].ToList();
            _inner = coordinates.Skip(1).ToList();
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds <paramref name="point"/> to the polygon.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        public void Add(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _outer.Add(point);
        }

        /// <summary>
        /// Inserts <paramref name="point"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="point">The point to be added.</param>
        public void Insert(int index, IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _outer.Insert(index, point);
        }

        /// <summary>
        /// Removes the specified <paramref name="point"/> from the polygon.
        /// </summary>
        /// <param name="point">The point to be removed.</param>
        public void Remove(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _outer.Remove(point);
        }

        /// <summary>
        /// Removes the point at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the point to be removed.</param>
        public void Remove(int index) {
            _outer.RemoveAt(index);
        }

        /// <summary>
        /// Removes all points from the polygon.
        /// </summary>
        public void Clear() {
            _outer.Clear();
        }

        /// <summary>
        /// Gets whether the polygon contains the point defined by the specified <paramref name="latitude"/> and <paramref name="longitude"/>.
        /// </summary>
        /// <param name="latitude">The langitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <returns><c>true</c> if the polygon contains the point; otheerwise <c>false</c>.</returns>
        public bool Contains(double latitude, double longitude) {
            return Contains(new Point(latitude, longitude));
        }

        /// <summary>
        /// Gets whether the polygon contains the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if the polygon contains <paramref name="point"/>; otheerwise <c>false</c>.</returns>
        public bool Contains(IPoint point) {
            bool contains = MapsUtils.IsPointInPolygon(Outer, point);
            if (contains == false) return false;
            return Inner.Length == 0 || Inner.Any(x => MapsUtils.IsPointInPolygon(x, point) == false);
        }

        /// <summary>
        /// Gets an instance of <see cref="IPoint"/> representing the center of the polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint GetCenter() {
            return GetBoundingBox().GetCenter();
        }

        /// <summary>
        /// Gets the area of the polygon in square metres. 
        /// </summary>
        /// <returns>The area in square metres.</returns>
        public double GetArea() {
            return MapsUtils.GetArea(Outer);
        }

        /// <summary>
        /// Gets the circumference of the polygon in metres. 
        /// </summary>
        /// <returns>The circumference in metres.</returns>
        public double GetCircumference() {
            return MapsUtils.GetCircumference(Outer);
        }

        /// <summary>
        /// Gets an instance of <see cref="IRectangle"/> representing the bounding box of the polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        public IRectangle GetBoundingBox() {
            return MapsUtils.GetBoundingBox(Outer);
        }

        ///// <summary>
        ///// Gets an instance of <see cref="WktPolygon"/> representing the polygon as described by the <strong>Well Known Text</strong> format.
        ///// </summary>
        ///// <returns>An instance of <see cref="WktPolygon"/>.</returns>
        //public WktPolygon ToWellKnownText() {
        //    return new WktPolygon(
        //        from point in Outer
        //        select point.ToWellKnownText()
        //    );
        //}

        #endregion

        #region Static methods

        /// <summary>
        /// Loads a polygon from the file at the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>An instance of <see cref="Polygon"/>.</returns>
        public static Polygon Load(string path) {
            return new Polygon(
                from line in System.IO.File.ReadLines(path)
                where string.IsNullOrWhiteSpace(line) == false
                let pieces = line.Replace(", ", ",").Split(',')
                select new Point {
                    Latitude = double.Parse(pieces[0], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(pieces[1], CultureInfo.InvariantCulture)
                }
            );
        }

        ///// <summary>
        ///// Initializes a new <see cref="Polygon"/> from the specified <paramref name="polygon"/>.
        ///// </summary>
        ///// <param name="polygon">The polygon as described by the <strong>Well Known Text</strong> format.</param>
        ///// <returns>An instance of <see cref="Polygon"/>.</returns>
        //public static Polygon FromWellKnownText(WktPolygon polygon) {
        //    return new Polygon(from p in polygon.Outer select HestExtensions.ToPoint(p));
        //}
        
        #endregion

    }

}