using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt {

    /// <summary>
    /// Class representing a <strong>Well Known Text</strong> multi point geometry.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry</cref>
    /// </see>
    public class WktMultiPoint : WktGeometry, IReadOnlyList<WktPoint> {

        private readonly List<WktPoint> _points;

        #region Properties

        /// <summary>
        /// Gets the number of points in this multi point geometry.
        /// </summary>
        public int Count => _points.Count;

        /// <summary>
        /// Returns the point at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the point to retrieve.</param>
        /// <returns>The <see cref="WktPoint"/> at the specified <paramref name="index"/>.</returns>
        public WktPoint this[int index] => _points[index];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an empty multi point geometry.
        /// </summary>
        public WktMultiPoint() {
            _points = new List<WktPoint>();
        }

        /// <summary>
        /// Initializes a new instance based on the specified array of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points that should make up the multi point geometry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public WktMultiPoint(WktPoint[] points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points = new List<WktPoint>(points);
        }

        /// <summary>
        /// Initializes a new instance based on the specified collection of <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points that should make up the multi point geometry.</param>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public WktMultiPoint(IEnumerable<WktPoint> points) {
            if (points == null) throw new ArgumentNullException(nameof(points));
            _points = new List<WktPoint>(points);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="point"/> to this multi point geometry.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
        public void Add(WktPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            _points.Add(point);
        }

        /// <summary>
        /// Removes the specified <paramref name="point"/> from this multi point geometry.
        /// </summary>
        /// <param name="point">The point to be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
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
        /// Returns the <strong>Well Known Text</strong> string representation of this multi point geometry.
        /// </summary>
        /// <returns>The multi point geometry formatted as a <strong>Well Known Text</strong> string.</returns>
        public override string ToString() {
            return ToString(WktFormatting.Default);
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of this multi point geometry.
        /// </summary>
        /// <param name="formatting">The formatting to be used.</param>
        /// <returns>The multi point geometry formatted as a <strong>Well Known Text</strong> string.</returns>
        public string ToString(WktFormatting formatting) {

            StringBuilder sb = new();

            sb.Append("MULTIPOINT");

            if (_points.Count == 0) {

                sb.Append(" EMPTY");

            } else {

                if (formatting != WktFormatting.Minified) sb.Append(" ");

                sb.Append("(" + string.Join(", ", from point in _points select string.Format(CultureInfo.InvariantCulture, "{0} {1}", point.X, point.Y)) + ")");

            }

            return sb.ToString();

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="input"/> string to an instance of <see cref="WktMultiPoint"/>.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>An instance of <seealso cref="WktMultiPoint"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
        /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
        public static new WktMultiPoint Parse(string input) {

            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

            input = input.Trim();

            if (!input.StartsWith("MULTIPOINT")) throw new WktInvalidFormatException(input);

            if (input.Equals("MULTIPOINT EMPTY")) return new WktMultiPoint();

            if (input.StartsWith("MULTIPOINT ((")) {
                input = input.Substring(12);
                input = input.Substring(0, input.Length - 1);
                input = input.Replace("(", "");
                input = input.Replace(")", "");
            } else if (input.StartsWith("MULTIPOINT((")) {
                input = input.Substring(11);
                input = input.Substring(0, input.Length - 1);
                input = input.Replace("(", "");
                input = input.Replace(")", "");
            }

            if (input.StartsWith("MULTIPOINT ")) input = input.Substring(8);
            if (input.StartsWith("MULTIPOINT")) input = input.Substring(7);

            MatchCollection matches = Regex.Matches(input, "([0-9\\.]+ [0-9\\.]+)");
            if (matches.Count == 0) throw new WktInvalidFormatException(input);

            return new WktMultiPoint(
                from Match match in matches
                select WktPoint.Parse(match.Groups[1].Value)
            );

        }

        #endregion

    }

}