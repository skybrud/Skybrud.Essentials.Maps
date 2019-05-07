using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Skybrud.Essentials.Maps.Geometry;

namespace Skybrud.Essentials.Maps.Wkt {

    public class WktPoint : WktShape {

        #region Properties

        public double X { get; }

        public double Y { get; }

        #endregion

        #region Constructors

        public WktPoint() { }

        public WktPoint(double x, double y) {
            X = x;
            Y = y;
        }

        public WktPoint(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            X = point.Longitude;
            Y = point.Latitude;
        }

        #endregion

        #region Member methods

        public override string ToString() {
            if (Math.Abs(X) < double.Epsilon && Math.Abs(Y) < double.Epsilon) return "POINT EMPTY";
            return string.Format(CultureInfo.InvariantCulture, "POINT ({0} {1})", X, Y);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a point from the specified <paramref name="str"/>.
        /// </summary>
        /// <param name="str">The string representing the point.</param>
        /// <returns>An instance of <see cref="WktPoint"/>.</returns>
        public new static WktPoint Parse(string str) {

            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            if (str.Equals("POINT EMPTY", StringComparison.CurrentCultureIgnoreCase)) return new WktPoint();

            Match m1 = Regex.Match(str.Trim(), "^([0-9\\.]+) ([0-9\\.]+)$");
            Match m2 = Regex.Match(str.Replace("POINT (", "POINT(").Trim(), "^POINT\\(([0-9\\.]+) ([0-9\\.]+)\\)$");

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
            
            throw new Exception("Input string is in an invalid format");

        }

        #endregion

    }

}