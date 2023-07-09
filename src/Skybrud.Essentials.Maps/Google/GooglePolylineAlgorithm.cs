using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Lines;
using Skybrud.Essentials.Maps.Geometry.Shapes;

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable InconsistentNaming

namespace Skybrud.Essentials.Maps.Google {

    /// <summary>
    /// Static class for encoding and decoding points using Google's Polyline Algorithm.
    /// </summary>
    /// <see>
    ///     <cref>https://developers.google.com/maps/documentation/utilities/polylinealgorithm</cref>
    /// </see>
    /// <see>
    ///     <cref>https://gist.github.com/shinyzhu/4617989</cref>
    /// </see>
    public static class GooglePolylineAlgoritm {

        /// <summary>
        /// Encodes the specified <paramref name="shape"/> using Google's polyline algorithm.
        /// </summary>
        /// <param name="shape">The shape to be encoded.</param>
        /// <returns>The encoded string of coordinates.</returns>
        /// <remarks>
        /// As the name suggests, the polyline algorithm is for encoding polylines, so not all types implementing
        /// <see cref="IShape"/> are supported.
        ///
        /// When <paramref name="shape"/> is an instance of <see cref="IPolygon"/>, the outer bounds of the polygon are
        /// encoded. Any inner bounds defined in the polygon are ignored.
        ///
        /// When <paramref name="shape"/> is an instance of <see cref="IMultiPolygon"/>, the outer bounds of each
        /// polygon are encoded, and each set of coordinates are placed on their own line in the output string. Similar
        /// to when encoding a single polygon, the inner bounds of the polygons are ignored. Multi polygon shapes are
        /// not directly supported by the polyline algoritm, so the encoded results for each polygon are placed on
        /// separate lines.
        ///
        /// Other types will throw an exception saying that the type is not supported.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="shape"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="shape"/> is not a supported type.</exception>
        public static string Encode(IShape shape) {
            if (shape == null) throw new ArgumentNullException(nameof(shape));
            return shape switch {
                IPolygon polygon => Encode(polygon),
                IMultiPolygon multiPolygon => Encode(multiPolygon),
                _ => throw new InvalidOperationException("Unsupported type: " + shape.GetType())
            };
        }

        /// <summary>
        /// Encodes the specified <paramref name="point"/> using Google's polyline algorithm.
        /// </summary>
        /// <param name="point">The point to be encoded.</param>
        /// <returns>The encoded string of coordinates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="point"/> is <c>null</c>.</exception>
        public static string Encode(IPoint point) {
            if (point == null) throw new ArgumentNullException(nameof(point));
            return Encode(new []{ point });
        }

        /// <summary>
        /// Encodes the specified <paramref name="polygon"/> using Google's polyline algorithm.
        /// </summary>
        /// <param name="polygon">The polygon to be encoded.</param>
        /// <returns>The encoded string of coordinates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="polygon"/> is <c>null</c>.</exception>
        public static string Encode(IPolygon polygon) {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));
            return Encode(polygon.Outer);
        }

        /// <summary>
        /// Encodes the specified <paramref name="multiPolygon"/> using Google's polyline algorithm.
        /// </summary>
        /// <param name="multiPolygon">A collection of polygons to be encoded.</param>
        /// <returns>The encoded string of coordinates.</returns>
        /// <remarks>The polyline algorithm doesn't directly support multiple polygons and the encoded strings for each polygons
        /// are therefore separated by <see cref="Environment.NewLine"/>.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="multiPolygon"/> is <c>null</c>.</exception>
        public static string Encode(IMultiPolygon multiPolygon) {
            if (multiPolygon == null) throw new ArgumentNullException(nameof(multiPolygon));
            return string.Join(Environment.NewLine, from polygon in multiPolygon.Polygons select Encode(polygon));
        }

        /// <summary>
        /// Encodes the specified <paramref name="lineString"/> using Google's polyline algorithm.
        /// </summary>
        /// <param name="lineString">The line string to be encoded.</param>
        /// <returns>The encoded string of coordinates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="lineString"/> is <c>null</c>.</exception>
        public static string Encode(ILineString lineString) {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));
            return Encode(lineString.Points);
        }

        /// <summary>
        /// Encodes the specified <paramref name="points"/> using Google's polyline algorithm.
        /// </summary>
        /// <param name="points">The points to be encoded.</param>
        /// <returns>The encoded string of coordinates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="points"/> is <c>null</c>.</exception>
        public static string Encode(IEnumerable<IPoint> points) {

            StringBuilder str = new();

            void EncodeDiff(int diff) {

                int shifted = diff << 1;
                if (diff < 0) shifted = ~shifted;

                int rem = shifted;

                while (rem >= 0x20) {
                    str.Append((char) ((0x20 | (rem & 0x1f)) + 63));
                    rem >>= 5;
                }

                str.Append((char) (rem + 63));

            }

            int lastLat = 0;
            int lastLng = 0;

            foreach (IPoint point in points) {

                int lat = (int) Math.Round(point.Latitude * 1E5);
                int lng = (int) Math.Round(point.Longitude * 1E5);

                EncodeDiff(lat - lastLat);
                EncodeDiff(lng - lastLng);

                lastLat = lat;
                lastLng = lng;

            }

            return str.ToString();

        }

        /// <summary>
        /// Decodes the specified string of <paramref name="encodedPoints"/> into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <param name="encodedPoints">The encoded string to be decoded.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="encodedPoints"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException"><typeparamref name="T"/> is not a supported type.</exception>
        public static T Decode<T>(string encodedPoints) where T : IGeometry {

            if (string.IsNullOrWhiteSpace(encodedPoints)) throw new ArgumentNullException(nameof(encodedPoints));

            Type type = typeof(T);

            switch (type.FullName) {

                case "Skybrud.Essentials.Maps.Geometry.IPoint":
                case "Skybrud.Essentials.Maps.Geometry.Point":
                    return (T) Decode(encodedPoints).First();

                case "Skybrud.Essentials.Maps.Geometry.Lines.ILineString":
                case "Skybrud.Essentials.Maps.Geometry.Lines.LineString":
                    return (T) (object) new LineString(Decode(encodedPoints));

                case "Skybrud.Essentials.Maps.Geometry.Shapes.IPolygon":
                case "Skybrud.Essentials.Maps.Geometry.Shapes.Polygon":
                    return (T) (object) new Polygon(Decode(encodedPoints));

                case "Skybrud.Essentials.Maps.Geometry.Shapes.IMultiPolygon":
                case "Skybrud.Essentials.Maps.Geometry.Shapes.MultiPolygon":
                    return (T) (object) new MultiPolygon(
                        from line in encodedPoints.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                        select new Polygon(Decode(line))
                    );

                default:
                    throw new InvalidOperationException("Unsupported type " + type.FullName);

            }

        }

        /// <summary>
        /// Decodes a string of coordinates encoded with Google's polyline algorithm.
        /// </summary>
        /// <param name="encodedPoints">The encoded string.</param>
        /// <returns>A collection of <see cref="IPoint"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="encodedPoints"/> is <c>null</c>.</exception>
        public static IEnumerable<IPoint> Decode(string encodedPoints) {

            if (string.IsNullOrEmpty(encodedPoints)) throw new ArgumentNullException(nameof(encodedPoints));

            char[] polylineChars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylineChars.Length) {

                // calculate next latitude
                sum = 0;
                shifter = 0;
                do {
                    next5bits = polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do {
                    next5bits = polylineChars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylineChars.Length);

                if (index >= polylineChars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                yield return new Point {
                    Latitude = Convert.ToDouble(currentLat) / 1E5,
                    Longitude = Convert.ToDouble(currentLng) / 1E5
                };

            }

        }

    }

}