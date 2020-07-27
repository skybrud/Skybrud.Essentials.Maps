using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Skybrud.Essentials.Maps.Geometry.Shapes {

    /// <summary>
    /// Class representing a collection of polygons.
    /// </summary>
    public class MultiPolygon : IMultiPolygon {

        private readonly List<IPolygon> _polygons;

        #region Properties

        /// <summary>
        /// Gets the array of polygons making up the shape.
        /// </summary>
        public IPolygon[] Polygons => _polygons.ToArray();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance from the specified array of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">The array of polygons.</param>
        public MultiPolygon(params IPolygon[] polygons) {
            _polygons = polygons?.ToList() ?? throw new ArgumentNullException(nameof(polygons));
        }

        /// <summary>
        /// Initializes a new instance from the specified collection of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">The collection of polygons.</param>
        public MultiPolygon(IEnumerable<IPolygon> polygons) {
            _polygons = polygons?.ToList() ?? throw new ArgumentNullException(nameof(polygons));
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="polygon"/> to the collection.
        /// </summary>
        /// <param name="polygon">The polygon to be added.</param>
        public void Add(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            _polygons.Add(polygon);
        }

        /// <summary>
        /// Inserts <paramref name="polygon"/> ad the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index at which <paramref name="polygon"/> should be added.</param>
        /// <param name="polygon">The polygon to be added.</param>
        public void Insert(int index, IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            _polygons.Insert(index, polygon);
        }

        /// <summary>
        /// Removes the specified <paramref name="polygon"/> from the collection.
        /// </summary>
        /// <param name="polygon">The collection to be removed.</param>
        public void Remove(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            _polygons.Remove(polygon);
        }

        /// <summary>
        /// Removes the polygon at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index at which the polygon should be removed.</param>
        public void Remove(int index) {
            _polygons.RemoveAt(index);
        }

        /// <summary>
        /// Clears the collection of polygons.
        /// </summary>
        public void Clear() {
            _polygons.Clear();
        }

        /// <summary>
        /// Returns whether this multi polygon contains the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if this multi polygon contains <paramref name="point"/>; otherwise <c>false</c>.</returns>
        public bool Contains(IPoint point) {
            return Polygons.Any(x => x.Contains(point));
        }

        /// <summary>
        /// Returns the center point (centroid) of this multi polygon. There is no guarentee that the center point is inside the multi polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IPoint"/>.</returns>
        public IPoint GetCenter() {
            return GetBoundingBox().GetCenter();
        }

        /// <summary>
        /// Returns the total area of the multi polygon, calculated in square metres.
        /// </summary>
        /// <returns>The total area.</returns>
        public double GetArea() {
            return _polygons.Count == 0 ? 0 : _polygons.Sum(x => x.GetArea());
        }

        /// <summary>
        /// Returns the total circumference of the multi polygon, calculated in metres.
        /// </summary>
        /// <returns>The total circumference.</returns>
        public double GetCircumference() {
            return _polygons.Count == 0 ? 0 : _polygons.Sum(x => x.GetCircumference());
        }

        /// <summary>
        /// Returns a new rectangle representing the bounding box of this multi polygon.
        /// </summary>
        /// <returns>An instance of <see cref="IRectangle"/>.</returns>
        public IRectangle GetBoundingBox() {
            return MapsUtils.GetBoundingBox(Polygons.SelectMany(x => x.Outer));
        }

        /// <summary>
        /// Returns an that iterates through the underlying <see cref="List{IPolygon}"/>.
        /// </summary>
        /// <returns>A <see cref="List{IPolygon}.Enumerator"/> for the underlying <see cref="List{IPolygon}"/>.</returns>
        public IEnumerator<IPolygon> GetEnumerator() {
            return _polygons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

    }

}