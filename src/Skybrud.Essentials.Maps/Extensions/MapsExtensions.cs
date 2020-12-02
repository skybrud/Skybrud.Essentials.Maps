using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Maps.Geometry;
using Skybrud.Essentials.Maps.Geometry.Shapes;

namespace Skybrud.Essentials.Maps.Extensions {

    /// <summary>
    /// Static class with various extension methods for use with this package.
    /// </summary>
    public static class MapsExtensions {

        /// <summary>
        /// Returns a two-dimensional array representing the points of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A two-dimensional array of <see cref="IPoint"/>.</returns>
        public static IPoint[][] GetCoordinates(this IPolygon polygon) {
            List<IPoint[]> temp = new List<IPoint[]> { polygon.Outer };
            temp.AddRange(polygon.Inner);
            return temp.ToArray();
        }

        /// <summary>
        /// Returns whether the point with the specified <paramref name="latitude"/> and <paramref name="longitude"/> is cointained by <see cref="IShape"/>.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <returns><c>true</c> if <paramref name="shape"/> contains the point; otherwise <c>false</c>.</returns>
        public static bool Contains(this IShape shape, double latitude, double longitude) {
            return shape.Contains(new Point(latitude, longitude));
        }

        /// <summary>
        /// Returns an array with the X and Y coordinates of the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point to bee converted.</param>
        /// <returns>An array of <see cref="double"/> representing the coordinates of <paramref name="point"/>.</returns>
        public static double[] ToXyArray(this IPoint point) {
            return new[] {point.Longitude, point.Latitude};
        }

        /// <summary>
        /// Returns an array representing the <strong>y</strong> and <strong>x</strong> coordinates of the specified <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>An array with the <strong>y</strong> and <strong>x</strong> coordinates of <paramref name="point"/>.</returns>
        public static double[] ToYxArray(this IPoint point) {
            return new[] {point.Latitude, point.Longitude};
        }

        /// <summary>
        /// Returns a two-dimensional array with the <strong>y</strong> and <strong>x</strong> coordinates of the specified <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>A two-dimensional array with the <strong>y</strong> and <strong>x</strong> coordinates of <paramref name="rectangle"/>.</returns>
        public static double[][] ToYxArray(this IRectangle rectangle) {
            return new[] { rectangle.SouthWest.ToYxArray(), rectangle.NorthEast.ToYxArray() };
        }

        /// <summary>
        /// Returns a three-dimensional array representing the outer and inner bounds of the specified <paramref name="polygon"/>.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <returns>A three-dimensional array with the <strong>y</strong> and <strong>x</strong> coordinates of <paramref name="polygon"/>.</returns>
        public static double[][][] ToYxArray(this IPolygon polygon) {

            double[][][] result = new double[polygon.Inner.Length + 1][][];

            result[0] = polygon.Outer.Select(x => x.ToYxArray()).ToArray();

            for (int i = 0; i<polygon.Inner.Length; i++) {
                result[i + 1] = polygon.Inner[i].Select(x => x.ToYxArray()).ToArray();
            }

            return result;

        }

        /// <summary>
        /// Returns the center point of the specified collection of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">A collection of polygons.</param>
        /// <returns>An instance of <see cref="IPoint"/> representing the center point.</returns>
        public static IPoint GetCenter(this IEnumerable<IPolygon> polygons) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            return MapsUtils.GetBoundingBox(polygons.SelectMany(x => x.Outer)).GetCenter();
        }

        /// <summary>
        /// Returns the bounding box of the specified collection of <paramref name="polygons"/>.
        /// </summary>
        /// <param name="polygons">A collection of polygons.</param>
        /// <returns>An instance of <see cref="IRectangle"/> representing the bounding box.</returns>
        public static IRectangle GetBoundingBox(this IEnumerable<IPolygon> polygons) {
            if (polygons == null) throw new ArgumentNullException(nameof(polygons));
            return MapsUtils.GetBoundingBox(polygons.SelectMany(x => x.Outer));
        }

        /// <summary>
        /// Converts the specified <paramref name="rectangle"/> to a new polygon, based on the corner points.
        /// </summary>
        /// <param name="rectangle">The rectangle to be converted.</param>
        /// <returns>A new instance of <see cref="IPolygon"/>.</returns>
        public static IPolygon ToPolygon(this IRectangle rectangle) {
            if (rectangle == null) throw new ArgumentNullException(nameof(rectangle));
            return new Polygon(rectangle);
        }

        /// <summary>
        /// Converts the specified <paramref name="circle"/> to a polygon.
        /// </summary>
        /// <param name="circle">The circle to be converted.</param>
        /// <returns>A new instance of <see cref="IPolygon"/>.</returns>
        public static IPolygon ToPolygon(this ICircle circle) {
            return ToPolygon(circle, 0.25f);
        }

        /// <summary>
        /// Converts the specified <paramref name="circle"/> to a polygon.
        /// </summary>
        /// <param name="circle">The circle to be converted.</param>
        /// <param name="delta"></param>
        /// <returns>A new instance of <see cref="IPolygon"/>.</returns>
        public static IPolygon ToPolygon(this ICircle circle, float delta) {

            List<IPoint> path = new List<IPoint>();

            for (float i = 0; i <= 360; i += delta) {
                path.Add(MapsUtils.ComputeOffset(circle.Center, circle.Radius, i));
            }

            return new Polygon(path);

        }

    }

}