using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry.Lines;

namespace Skybrud.Essentials.Maps.Wkt {

    /// <summary>
    /// Class representing a <strong>Well Known Text</strong> line string.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry</cref>
    /// </see>
    public class WktLineString : WktGeometry, IReadOnlyList<WktPoint> {

        private readonly List<WktPoint> _points;

        #region Properties

        /// <summary>
        /// Gets the number of points in this line string.
        /// </summary>
        public int Count => _points.Count;

        /// <summary>
        /// Gets the <see cref="WktPoint"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="WktPoint"/> at the specified index.</returns>
        public WktPoint this[int index] => _points[index];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an empty line string.
        /// </summary>
        public WktLineString() {
            _points = new List<WktPoint>();
        }

        /// <summary>
        /// Initializes a new line string based on the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points that should make up the line string.</param>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public WktLineString(WktPoint[] points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points = new List<WktPoint>(points);
        }

        /// <summary>
        /// Initializes a new WKT line string based on the specified <paramref name="lineString"/>.
        /// </summary>
        /// <param name="lineString">The shape that should make up the line string.</param>
        /// <exception cref="ArgumentNullException"><paramref name="lineString"/> is <c>null</c>.</exception>
        public WktLineString(ILineString lineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            _points = new List<WktPoint>(lineString.Points.Select(WktUtils.ToWkt));
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="point"/> to this line string.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
        public void Add(WktPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(point);
        }

        /// <summary>
        /// Removes the specified <paramref name="point"/> from this line string.
        /// </summary>
        /// <param name="point">The point to be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
        /// <returns><c>true</c> if <paramref name="point"/> is successfully removed; otherwise, <c>false</c>.</returns>
        public bool Remove(WktPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            return _points.Remove(point);
        }

        /// <inheritdoc />
        public IEnumerator<WktPoint> GetEnumerator() {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of this line string.
        /// </summary>
        /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
        public override string ToString() {
            return ToString(WktFormatting.Default);
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of this line string.
        /// </summary>
        /// <param name="formatting">The formatting to be used.</param>
        /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
        public string ToString(WktFormatting formatting) {
            return "LINESTRING " + (_points.Count == 0 ? "EMPTY" : ToString(this, formatting, 0));
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="input"/> string to an instance of <see cref="WktLineString"/>.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>An instance of <seealso cref="WktLineString"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
        public static new WktLineString Parse(string input) {

            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

            input = input.Trim();

            if (input.Equals("LINESTRING EMPTY")) return new WktLineString();

            if (input.StartsWith("LINESTRING")) input = input.Substring(10).Trim();

            return new WktLineString(ParsePoints(input));

        }

        #endregion

    }

}