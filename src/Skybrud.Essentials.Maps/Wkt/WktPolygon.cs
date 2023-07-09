using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Skybrud.Essentials.Collections;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt {

    /// <summary>
    /// Class representing a <strong>Well Known Text</strong> polygon.
    /// </summary>
    /// <see>
    ///     <cref>https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry</cref>
    /// </see>
    public class WktPolygon : WktGeometry {

        #region Properties

        /// <summary>
        /// Gets a array with the outer coordinates.
        /// </summary>
        public WktPoint[] Outer => Coordinates[0];

        /// <summary>
        /// Gets a array with the inner coordinates.
        /// </summary>
        public WktPoint[][] Inner => Coordinates.Skip(1).ToArray();

        /// <summary>
        /// Gets a array with the coordinates of the polygon.
        /// </summary>
        public WktPoint[][] Coordinates { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty polygon.
        /// </summary>
        public WktPolygon() {
            Coordinates = new WktPoint[1][];
            Coordinates[0] = ArrayUtils.Empty<WktPoint>();
        }

        /// <summary>
        /// Initializes a new from the specified <paramref name="polygon"/>.
        /// </summary>
        public WktPolygon(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            Coordinates = WktUtils.ToWktPoints(polygon);
        }

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="outer"/> coordinates.
        /// </summary>
        /// <param name="outer">The outer coordinates.</param>
        public WktPolygon(WktPoint[] outer) {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            Coordinates = new[] { outer};
        }

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="outer"/> coordinates.
        /// </summary>
        /// <param name="outer">The outer coordinates.</param>
        public WktPolygon(IEnumerable<WktPoint> outer) {
            if (outer == null) throw new ArgumentNullException(nameof(outer));
            Coordinates = new[] { outer.ToArray() };
        }

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="outer"/>  and <paramref name="inner"/> coordinates.
        /// </summary>
        /// <param name="outer">The outer coordinates.</param>
        /// <param name="inner">The inner coordinates.</param>
        public WktPolygon(WktPoint[] outer, WktPoint[][] inner) {

            Coordinates = new WktPoint[inner.Length + 1][];
            Coordinates[0] = outer;

            for (int i = 0; i < inner.Length; i++) {
                Coordinates[i + 1] = inner[i];
            }

        }

        /// <summary>
        /// Initializes a new instance from the specified array of <paramref name="coordinates"/>
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public WktPolygon(WktPoint[][] coordinates) {
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
        }

        /// <summary>
        /// Initializes a new instance from the specified array of <see cref="IPoint"/> <paramref name="coordinates"/>.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public WktPolygon(IPoint[][] coordinates) {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            Coordinates = new WktPoint[coordinates.Length][];
            for (int i = 0; i < coordinates.Length; i++) {
                Coordinates[i] = new WktPoint[coordinates[i].Length];
                for (int j = 0; j < Coordinates[i].Length; j++){
                    Coordinates[i][j] = new WktPoint(coordinates[i][j]);
                }
            }
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of this polygon.
        /// </summary>
        /// <returns>The polygon formatted as a <strong>Well Known Text</strong> string.</returns>
        public override string ToString() {
            return ToString(WktFormatting.Default);
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of this polygon.
        /// </summary>
        /// <param name="formatting">The formatting to be used.</param>
        /// <returns>The polygon formatted as a <strong>Well Known Text</strong> string.</returns>
        public string ToString(WktFormatting formatting) {

            StringBuilder sb = new();

            sb.Append("POLYGON");

            if (formatting != WktFormatting.Minified || Outer.Length == 0) {
                sb.Append(" ");
            }

            sb.Append(Outer.Length == 0 ? "EMPTY" : ToString(this, formatting, 0));

            return sb.ToString();

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a new isnstance from the specified Well Known Text <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The string to be parsed.</param>
        /// <returns>An instacne of <see cref="WktPolygon"/>.</returns>
        /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
        public static new WktPolygon Parse(string input) {

            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

            input = input.Trim();
            if (input.Equals("POLYGON EMPTY")) return new WktPolygon();
            if (input.StartsWith("POLYGON")) input = input.Substring(7).Trim();

            MatchCollection matches = Regex.Matches(input, "\\(([0-9\\., ]+)\\)");
            if (matches.Count == 0) throw new WktInvalidFormatException(input);

            List<WktPoint[]> inner = new();

            for (int i = 1; i < matches.Count; i++) {
                inner.Add(ParsePoints(matches[i].Groups[1].Value));
            }

            WktPoint[] outer = ParsePoints(matches[0].Groups[1].Value);

            return new WktPolygon(outer, inner.ToArray());

        }

        #endregion

    }

}