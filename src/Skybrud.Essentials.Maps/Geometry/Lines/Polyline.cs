using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Geometry.Lines {

    /// <summary>
    /// Class representing a polyline (closed path).
    /// </summary>
    public class Polyline : IPolyline {

        private readonly List<IPoint> _points;

        #region Properties

        /// <summary>
        /// Gets the array of points making up the polyline.
        /// </summary>
        public IPoint[] Points => _points.ToArray();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new polyline from the specified array of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the polyline should be based on.</param>
        public Polyline(params IPoint[] points) {
            _points = points?.ToList() ?? throw new ArgumentNullException(nameof(points));
        }

        /// <summary>
        /// Initializes a new polyline from the specified collection of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points the polyline should be based on.</param>
        public Polyline(IEnumerable<IPoint> points) {
            _points = points?.ToList() ?? throw new ArgumentNullException(nameof(points));
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds <paramref name="point"/> to the polyline.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        public void Add(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(point);
        }

        /// <summary>
        /// Inserts <paramref name="point"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="point">The point to be added.</param>
        public void Insert(int index, Point point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Insert(index, point);
        }

        /// <summary>
        /// Removes the specified <paramref name="point"/> from the polyline.
        /// </summary>
        /// <param name="point">The point to be removed.</param>
        public void Remove(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Remove(point);
        }

        /// <summary>
        /// Removes the point at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the point to be removed.</param>
        public void Remove(int index) {
            _points.RemoveAt(index);
        }

        /// <summary>
        /// Removes all points from the polyline.
        /// </summary>
        public void Clear() {
            _points.Clear();
        }

        /// <summary>
        /// Gets the total length of polyline.
        /// </summary>
        /// <returns>The total length in metres.</returns>
        public double GetLength() {
            return MapsUtils.GetLength(_points);
        }

        public bool Contains(IPoint point) {
            throw new NotImplementedException();
        }

        public IPoint GetCenter() {
            return GetBoundingBox().GetCenter();
        }

        public double GetArea() {
            throw new NotImplementedException();
        }

        public double GetCircumference() {
            throw new NotImplementedException();
        }

        public IRectangle GetBoundingBox() {
            return new Rectangle(Points);
        }

        #endregion

    }

}