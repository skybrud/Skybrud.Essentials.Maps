using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt {

    /// <summary>
    /// Class representing a <strong>Well Known Text</strong> point.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry</cref>
    /// </see>
    public class WktPoint : WktGeometry {

        #region Properties

        /// <summary>
        /// Gets or sets the coordinate on the X-axis.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the coordinate on the Y-axis.
        /// </summary>
        public double Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new point with default options.
        /// </summary>
        public WktPoint() { }

        /// <summary>
        /// Initializes a new point based on the specified <paramref name="x"/> and <paramref name="y"/> coordinates.
        /// </summary>
        /// <param name="x">The coordinate on the X-axis.</param>
        /// <param name="y">The coordinate on the Y-axis.</param>
        public WktPoint(double x, double y) {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">An instance of <see cref="IPoint"/> this point should be based on.</param>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
        public WktPoint(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            X = point.Longitude;
            Y = point.Latitude;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of this point.
        /// </summary>
        /// <returns>The point formatted as a <strong>Well Known Text</strong> string.</returns>
        public override string ToString() {
            if (Math.Abs(X) < double.Epsilon && Math.Abs(Y) < double.Epsilon) return "POINT EMPTY";
            return string.Format(CultureInfo.InvariantCulture, "POINT ({0} {1})", X, Y);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a point from the specified <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The string representing the point.</param>
        /// <returns>An instance of <see cref="WktPoint"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
        /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
        public static new WktPoint Parse(string input) {

            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

            if (input.Equals("POINT EMPTY", StringComparison.CurrentCultureIgnoreCase)) return new WktPoint();

            Match m1 = Regex.Match(input.Trim(), "^([0-9\\.]+) ([0-9\\.]+)$");
            Match m2 = Regex.Match(input.Replace("POINT (", "POINT(").Trim(), "^POINT\\(([0-9\\.]+) ([0-9\\.]+)\\)$");

            if (m1.Success) {
                double x = double.Parse(m1.Groups[1].Value, CultureInfo.InvariantCulture);
                double y = double.Parse(m1.Groups[2].Value, CultureInfo.InvariantCulture);
                return new WktPoint(x, y);
            }

            if (m2.Success) {
                double x = double.Parse(m2.Groups[1].Value, CultureInfo.InvariantCulture);
                double y = double.Parse(m2.Groups[2].Value, CultureInfo.InvariantCulture);
                return new WktPoint(x, y);
            }

            throw new WktInvalidFormatException(input);

        }

        #endregion

    }

}