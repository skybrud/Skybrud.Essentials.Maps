using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Skybrud.Essentials.Maps.Wkt.Exceptions;

namespace Skybrud.Essentials.Maps.Wkt {

    /// <summary>
    /// Base class representing a <strong>Well Known Text</strong> geometry.
    /// </summary>
    public abstract class WktGeometry {

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to be converted.</param>
        /// <param name="formatting">The formatting to be used.</param>
        /// <param name="indentation">The indendatation to be used.</param>
        /// <returns>The point formatted as a <strong>Well Known Text</strong> string.</returns>
        protected string ToString(WktPoint point, WktFormatting formatting, int indentation) {
            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");
            return string.Format(CultureInfo.InvariantCulture, "({0} {1})", point.X, point.Y);
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of the specified <paramref name="points"/>.
        /// </summary>
        /// <param name="points">The points to be converted.</param>
        /// <param name="formatting">The formatting to be used.</param>
        /// <param name="indentation">The indendatation to be used.</param>
        /// <returns>The points formatted as a <strong>Well Known Text</strong> string.</returns>
        protected string ToString(WktPoint[] points, WktFormatting formatting, int indentation) {
            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");
            return "(" + string.Join(formatting == WktFormatting.Minified ? "," : ", ", from point in points select string.Format(CultureInfo.InvariantCulture, "{0} {1}", point.X, point.Y)) + ")";
        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon to be converted.</param>
        /// <param name="formatting">The formatting to be used.</param>
        /// <param name="indentation">The indendatation to be used.</param>
        /// <returns>The polygon formatted as a <strong>Well Known Text</strong> string.</returns>
        protected string ToString(WktPolygon polygon, WktFormatting formatting, int indentation) {

            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");

            List<string> temp = new();

            temp.Add(ToString(polygon.Outer, formatting, indentation));

            foreach (WktPoint[] array in polygon.Inner) {
                temp.Add(ToString(array, formatting, indentation));
            }

            return "(" + string.Join(formatting == WktFormatting.Minified ? "," : ", ", temp) + ")";

        }

        /// <summary>
        /// Returns the <strong>Well Known Text</strong> string representation of the specified <paramref name="lineString"/>.
        /// </summary>
        /// <param name="lineString">The line string to be converted.</param>
        /// <param name="formatting">The formatting to be used.</param>
        /// <param name="indentation">The indendatation to be used.</param>
        /// <returns>The line string formatted as a <strong>Well Known Text</strong> string.</returns>
        protected string ToString(WktLineString lineString, WktFormatting formatting, int indentation) {
            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");
            return "(" + string.Join(formatting == WktFormatting.Indented ? "," : ", ", from point in lineString select string.Format(CultureInfo.InvariantCulture, "{0} {1}", point.X, point.Y)) + ")";
        }

        /// <summary>
        /// Parses the specified <paramref name="input"/> string into an instance of <see cref="WktGeometry"/>.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>An instance of <see cref="WktGeometry"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
        /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
        /// <exception cref="WktUnsupportedTypeException">type of <paramref name="input"/> is not supported.</exception>
        public static WktGeometry Parse(string input) {

            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

            string type = input.Split('(')[0].ToUpper().Trim();
            if (string.IsNullOrWhiteSpace(type)) throw new WktInvalidFormatException(input);

            return type switch {
                "POINT" => WktPoint.Parse(input),
                "POLYGON" => WktPolygon.Parse(input),
                "LINESTRING" => WktLineString.Parse(input),
                "MULTIPOINT" => WktMultiPoint.Parse(input),
                "MULTILINESTRING" => WktMultiLineString.Parse(input),
                "MULTIPOLYGON" => WktMultiPolygon.Parse(input),
                _ => throw new WktUnsupportedTypeException(type)
            };

        }

        /// <summary>
        /// Parses the specified <paramref name="input"/> string into an array of <see cref="WktPoint"/>.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <returns>An array of <see cref="WktPoint"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> is <c>null</c>.</exception>
        /// <exception cref="WktInvalidFormatException"><paramref name="input"/> is not in a known format.</exception>
        protected static WktPoint[] ParsePoints(string input) {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
            try {
                return input.Trim('(', ')').Split(',').Select(WktPoint.Parse).ToArray();
            } catch (Exception) {
                throw new WktInvalidFormatException(input);
            }
        }

    }

}