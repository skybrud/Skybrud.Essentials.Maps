using System;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry.Lines;

namespace Skybrud.Essentials.Maps.Wkt {

    public class WktLineString : WktShape {

        #region Properties

        /// <summary>
        /// Gets the array of points that make up the line string.
        /// </summary>
        public WktPoint[] Points { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an empty line string.
        /// </summary>
        public WktLineString() {
            Points = new WktPoint[0];
        }

        /// <summary>
        /// Initializes a new line string based on the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points that should make up the line string.</param>
        public WktLineString(WktPoint[] points) {
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }

        /// <summary>
        /// Initializes a new WKT line string based on the specified <paramref name="lineString"/>.
        /// </summary>
        /// <param name="lineString">The shape that should make up the line string.</param>
        public WktLineString(ILineString lineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            Points = lineString.Points.Select(WktUtils.ToWkt).ToArray();
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of the line string.
        /// </summary>
        /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
        public override string ToString() {
            return ToString(WktFormatting.Default);
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of the line string.
        /// </summary>
        /// <param name="formatting">The formatting to be used.</param>
        /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
        public string ToString(WktFormatting formatting) {
            return "LINESTRING " + (Points.Length == 0 ? "EMPTY" : ToString(this, formatting, 0));
        }

        #endregion

        #region Static methods

        public new static WktLineString Parse(string str) {

            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            str = str.Trim();

            if (str.Equals("LINESTRING EMPTY")) return new WktLineString();

            if (str.StartsWith("LINESTRING")) str = str.Substring(10).Trim();

            return new WktLineString(ParsePoints(str));

        }

        #endregion

    }

}