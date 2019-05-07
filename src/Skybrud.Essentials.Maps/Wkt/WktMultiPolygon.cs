using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybrud.Essentials.Maps.Wkt {

    public class WktMultiPolygon : WktShape {

        #region Properties

        public WktPolygon[] Polygons { get; }

        #endregion

        #region Constructors
        public WktMultiPolygon() {
            Polygons = new WktPolygon[0];
        }

        public WktMultiPolygon(WktPolygon[] polygons) {
            Polygons = polygons ?? new WktPolygon[0];
        }

        public WktMultiPolygon(List<WktPolygon> polygons) {
            Polygons = polygons?.ToArray() ?? new WktPolygon[0];
        }

        public WktMultiPolygon(IEnumerable<WktPolygon> polygons) {
            Polygons = polygons?.ToArray() ?? new WktPolygon[0];
        }

        #endregion

        #region Member methods

        public override string ToString() {
            return ToString(WktFormatting.Default);
        }

        public string ToString(WktFormatting formatting) {

            StringBuilder sb = new StringBuilder();

            sb.Append("MULTIPOLYGON ");

            if (Polygons.Length == 0) {

                sb.Append("EMPTY");

            } else {
                
                sb.Append("(" + String.Join(formatting == WktFormatting.Minified ? "," : ", ", from polygon in Polygons select ToString(polygon, formatting, 1)) + ")");

            }

            return sb.ToString();

        }

        #endregion

        #region Static methods

        public new static WktMultiPolygon Parse(string str) {
            
            if (String.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            str = str.Trim();

            if (!str.StartsWith("MULTIPOLYGON")) throw new Exception("Input string is in an invalid format");

            if (str.Equals("MULTIPOLYGON EMPTY")) return new WktMultiPolygon();

            str = str.Substring(12).Trim().Replace("\n", " ");
            str = str.Substring(1, str.Length - 2);

            List<WktPolygon> polygons = new List<WktPolygon>();

            string temp = "";

            int c = 0;

            for (int i = 0; i < str.Length; i++) {

                char chr = str[i];

                if (chr == '(') {
                    c++;
                    temp += chr;
                } else if (chr == ')') {
                    c--;
                    temp += chr;
                    if (c == 0) {
                        polygons.Add(WktPolygon.Parse(temp));
                        temp = "";
                    }
                } else if (chr == ',' && c == 0) {
                    
                } else {
                    temp += chr;
                }

            }

            return new WktMultiPolygon(polygons);

        }

        #endregion

    }

}