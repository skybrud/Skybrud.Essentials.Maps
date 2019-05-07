using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Skybrud.Essentials.Maps.Wkt {

    public class WktMultiPoint : WktShape {

        #region Properties

        public WktPoint[] Points { get; }

        #endregion

        #region Constructors

        public WktMultiPoint() {
            Points = new WktPoint[0];
        }

        public WktMultiPoint(WktPoint[] points) {
            Points = points ?? new WktPoint[0];
        }

        public WktMultiPoint(IEnumerable<WktPoint> points) {
            Points = points?.ToArray() ?? new WktPoint[0];
        }

        #endregion

        #region Member methods

        public override string ToString() {

            StringBuilder sb = new StringBuilder();

            sb.Append("MULTIPOINT ");

            if (Points.Length == 0) {

                sb.Append("EMPTY");

            } else {
                
                sb.Append("(" + String.Join(", ", from point in Points select String.Format(CultureInfo.InvariantCulture, "{0} {1}", point.X, point.Y)) + ")");

            }

            return sb.ToString();

        }

        #endregion

        #region Static methods

        public new static WktMultiPoint Parse(string str) {
            
            if (String.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            str = str.Trim();

            if (!str.StartsWith("MULTIPOINT")) throw new Exception("Input string is in an invalid format");

            if (str.Equals("MULTIPOINT EMPTY")) return new WktMultiPoint();
            
            if (str.StartsWith("MULTIPOINT ((")) {
                str = str.Substring(12);
                str = str.Substring(0, str.Length - 1);
                str = str.Replace("(", "");
                str = str.Replace(")", "");
            } else if (str.StartsWith("MULTIPOINT((")) {
                str = str.Substring(11);
                str = str.Substring(0, str.Length - 1);
                str = str.Replace("(", "");
                str = str.Replace(")", "");
            }

            if (str.StartsWith("MULTIPOINT ")) str = str.Substring(8);
            if (str.StartsWith("MULTIPOINT")) str = str.Substring(7);

            MatchCollection matches = Regex.Matches(str, "([0-9\\.]+ [0-9\\.]+)");
            if (matches.Count == 0) throw new Exception("Input string is in an invalid format");

            return new WktMultiPoint(
                from Match match in matches
                select WktPoint.Parse(match.Groups[1].Value)
            );

        }

        #endregion

    }

}