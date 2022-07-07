using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Skybrud.Essentials.Maps.Kml.Geometry;

namespace Skybrud.Essentials.Maps.Kml {

    /// <summary>
    /// Static class with various KML related utils.
    /// </summary>
    public static class KmlUtils {

        public static KmlGeometry[] ParseGeometryChildren(XElement xml, XmlNamespaceManager namespaces) {

            List<KmlGeometry> temp = new();

            foreach (XElement element in xml.Elements()) {

                switch (element.Name.LocalName) {

                    case "extrude":
                    case "tessellate":
                    case "altitudeMode":
                        continue;

                    case "Point":
                        temp.Add(KmlPoint.Parse(element));
                        break;

                    case "LineString":
                        temp.Add(KmlLineString.Parse(element));
                        break;

                    case "Polygon":
                        temp.Add(KmlPolygon.Parse(element));
                        break;

                    case "MultiGeometry":
                        temp.Add(KmlMultiGeometry.Parse(element));
                        break;

                }

            }

            return temp.ToArray();

        }

        /// <summary>
        /// Returns the corresponding HEX value of the color with specified <paramref name="red"/>, <paramref name="green"/> and <paramref name="blue"/> values.
        /// </summary>
        /// <param name="red">The red of the color.</param>
        /// <param name="green">The green of the color.</param>
        /// <param name="blue">The blue of the color.</param>
        /// <returns>The HEX value.</returns>
        public static string RgbToHex(byte red, byte green, byte blue) {

            string r = red.ToString("x").PadLeft(2, '0');
            string g = green.ToString("x").PadLeft(2, '0');
            string b = blue.ToString("x").PadLeft(2, '0');

            return $"{r}{g}{b}";

        }

        /// <summary>
        /// Returns the corresponding HEX value of the color with specified <paramref name="red"/>, <paramref name="green"/>, <paramref name="blue"/> and <paramref name="alpha"/> values.
        /// </summary>
        /// <param name="red">The red of the color.</param>
        /// <param name="green">The green of the color.</param>
        /// <param name="blue">The blue of the color.</param>
        /// <param name="alpha">The alpha value of the color.</param>
        /// <returns>The HEX value.</returns>
        public static string RgbToHex(byte red, byte green, byte blue, float alpha) {

            // Clamp the alpha value
            alpha = Math.Max(0, Math.Min(1, alpha));

            string r = red.ToString("x").PadLeft(2, '0');
            string g = green.ToString("x").PadLeft(2, '0');
            string b = blue.ToString("x").PadLeft(2, '0');
            string a = (alpha * 255).ToString("x").PadLeft(2, '0');

            return $"{a}{b}{g}{r}";

        }

        public static bool TryParseHexColor(string str, out byte r, out byte g, out byte b, out float a) {

            a = 1;
            r = 0;
            g = 0;
            b = 0;
            
            // Return "false" if the string is empty
            if (string.IsNullOrWhiteSpace(str)) return false;

            // Strip a leading hashtag and convert to lowercase
            str = str.TrimStart('#').ToLower();

            // Time for some regex :D
            Match m1 = Regex.Match(str, "^([0-9a-f]{1})([0-9a-f]{1})([0-9a-f]{1})$");
            Match m2 = Regex.Match(str, "^([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})$");
            Match m3 = Regex.Match(str, "^([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})$");

            if (m1.Success) {
                byte.TryParse(m1.Groups[1].Value + m2.Groups[3].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b);
                byte.TryParse(m1.Groups[2].Value + m2.Groups[2].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out g);
                byte.TryParse(m1.Groups[3].Value + m2.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out r);
                return true;
            }

            if (m2.Success) {
                byte.TryParse(m2.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b);
                byte.TryParse(m2.Groups[2].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out g);
                byte.TryParse(m2.Groups[3].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out r);
                return true;
            }

            if (m3.Success) {
                byte.TryParse(m3.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out byte alpha);
                byte.TryParse(m3.Groups[2].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b);
                byte.TryParse(m3.Groups[3].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out g);
                byte.TryParse(m3.Groups[4].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out r);
                a = alpha / 255f;
                return true;
            }

            return false;

        }

    }

}