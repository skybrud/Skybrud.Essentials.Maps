using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Skybrud.Essentials.Maps.Wkt {

    public abstract class WktShape {

        protected string ToString(WktPoint point, WktFormatting formatting, int indentation) {
            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");
            return String.Format(CultureInfo.InvariantCulture, "({0} {1})", point.X, point.Y);
        }

        protected string ToString(WktPoint[] points, WktFormatting formatting, int indentation) {
            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");
            return "(" + String.Join(formatting == WktFormatting.Minified ? "," : ", ", from point in points select String.Format(CultureInfo.InvariantCulture, "{0} {1}", point.X, point.Y)) + ")";
        }

        protected string ToString(WktPolygon polygon, WktFormatting formatting, int indentation) {

            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");

            List<string> temp = new List<string>();

            temp.Add(ToString(polygon.Outer, formatting, indentation));

            foreach (WktPoint[] array in polygon.Inner) {
                temp.Add(ToString(array, formatting, indentation));
            }

            return "(" + String.Join(formatting == WktFormatting.Minified ? "," : ", ", temp) + ")";

        }

        protected string ToString(WktLineString lineString, WktFormatting formatting, int indentation) {
            if (formatting == WktFormatting.Indented) throw new NotImplementedException("Indented formatting is currently not supported");
            return "(" + String.Join(formatting == WktFormatting.Indented ? "," : ", ", from point in lineString.Points select String.Format(CultureInfo.InvariantCulture, "{0} {1}", point.X, point.Y)) + ")";
        }

        public static WktShape Parse(string str) {

            if (String.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            string type = str.Split('(')[0].ToUpper().Trim();

            switch (type){
                case "POINT": return WktPoint.Parse(str);
                case "POLYGON": return WktPolygon.Parse(str);
                case "LINESTRING": return WktLineString.Parse(str);
                case "MULTIPOINT": return WktMultiPoint.Parse(str);
                case "MULTILINESTRING": return WktMultiLineString.Parse(str);
                case "MULTIPOLYGON": return WktMultiPolygon.Parse(str);
                default: throw new Exception("Unknown shape type " + type);
            }

        }

        protected static WktPoint[] ParsePoints(string str) {
            try {
                return str.Trim('(', ')').Split(',').Select(WktPoint.Parse).ToArray();
            } catch (Exception) {
                throw new Exception("Input string is in an invalid format\r\n\r\n--" + str + "--");
            }
        }

    }

}