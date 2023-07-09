using System;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry;

// ReSharper disable InconsistentNaming

namespace Skybrud.Essentials.Maps {

    public static partial class MapsUtils {

        /// <summary>
        /// Static utility class for converting UTM coordinates to latitude and longitude.
        /// </summary>
        public class Utm {

            /// <summary>
            /// Converts the specified UTM coordinates <paramref name="utmX"/> (easting) and <paramref name="utmY"/>
            /// (northing) to the corresponding <paramref name="latitude"/> and <paramref name="longitude"/>.
            /// </summary>
            /// <param name="utmX">The UTM coordinate across the X-axis.</param>
            /// <param name="utmY">The UTM coordinate across the Y-axis.</param>
            /// <param name="utmZone">The UTM zone to be used for the conversion.</param>
            /// <param name="latitude">The latitude result of the conversion.</param>
            /// <param name="longitude">The longitude result of the conversion.</param>
            /// <see>
            ///     <cref>https://stackoverflow.com/a/24899378</cref>
            /// </see>
            /// <see>
            ///     <cref>https://en.wikipedia.org/wiki/Universal_Transverse_Mercator_coordinate_system</cref>
            /// </see>
            public static void ToLatLng(double utmX, double utmY, string utmZone, out double latitude, out double longitude) {

                bool isNorthHemisphere = utmZone.Last() >= 'N';

                var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
                var c_sa = 6378137.000000;
                var c_sb = 6356752.314245;
                var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
                var e2cuadrada = Math.Pow(e2, 2);
                var c = Math.Pow(c_sa, 2) / c_sb;
                var x = utmX - 500000;
                var y = isNorthHemisphere ? utmY : utmY - 10000000;

                var s = ((zone * 6.0) - 183.0);
                var lat = y / (6366197.724 * 0.9996);
                var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
                var a = x / v;
                var a1 = Math.Sin(2 * lat);
                var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
                var j2 = lat + (a1 / 2.0);
                var j4 = ((3 * j2) + a2) / 4.0;
                var j6 = (5 * j4 + a2 * Math.Pow((Math.Cos(lat)), 2)) / 3.0;
                var alfa = (3.0 / 4.0) * e2cuadrada;
                var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
                var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
                var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
                var b = (y - bm) / v;
                var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
                var eps = a * (1 - (epsi / 3.0));
                var nab = (b * (1 - epsi)) + lat;
                var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
                var delt = Math.Atan(senoheps / (Math.Cos(nab)));
                var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

                longitude = (delt / Math.PI) * 180 + s;
                latitude = (((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat))) / Math.PI) * 180; // era incorrecto el calculo

            }

            /// <summary>
            /// Converts the specified UTM coordinates <paramref name="utmX"/> (easting) and <paramref name="utmY"/>
            /// (northing) to a corresponding <see cref="IPoint"/> identifier by latitude and longitude.
            /// </summary>
            /// <param name="utmX">The UTM coordinate across the X-axis.</param>
            /// <param name="utmY">The UTM coordinate across the Y-axis.</param>
            /// <param name="utmZone">The UTM zone to be used for the conversion.</param>
            /// <returns>An instance of <see cref="IPoint"/> representing the result of the conversion.</returns>
            public static IPoint ToLatLng(double utmX, double utmY, string utmZone) {
                ToLatLng(utmX, utmY, utmZone, out double lat, out double lng);
                return new Point(lat, lng);
            }

        }

    }

}